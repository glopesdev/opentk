using OpenTK.Graphics;
using OpenTK.Platform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTK
{
    static class PlatformContext
    {
        /// <summary>
        /// Constructs a new GraphicsContext with the specified GraphicsMode and attaches it to the specified window.
        /// </summary>
        /// <param name="mode">The OpenTK.Graphics.GraphicsMode of the GraphicsContext.</param>
        /// <param name="window">The OpenTK.Platform.IWindowInfo to attach the GraphicsContext to.</param>
        public static GraphicsContext Create(GraphicsMode mode, IWindowInfo window)
        {
            return Create(mode, window, 1, 0, GraphicsContextFlags.Default);
        }

        /// <summary>
        /// Constructs a new GraphicsContext with the specified GraphicsMode, version and flags,  and attaches it to the specified window.
        /// </summary>
        /// <param name="mode">The OpenTK.Graphics.GraphicsMode of the GraphicsContext.</param>
        /// <param name="window">The OpenTK.Platform.IWindowInfo to attach the GraphicsContext to.</param>
        /// <param name="major">The major version of the new GraphicsContext.</param>
        /// <param name="minor">The minor version of the new GraphicsContext.</param>
        /// <param name="flags">The GraphicsContextFlags for the GraphicsContext.</param>
        /// <remarks>
        /// Different hardware supports different flags, major and minor versions. Invalid parameters will be silently ignored.
        /// </remarks>
        public static GraphicsContext Create(GraphicsMode mode, IWindowInfo window, int major, int minor, GraphicsContextFlags flags)
        {
            //TODO: Implement share contexts
            return Create(mode, window, shareContext: null, major, minor, flags);
        }

        /// <summary>
        /// Constructs a new GraphicsContext with the specified GraphicsMode, version and flags, and attaches it to the specified window. A dummy context will be created if both
        /// the handle and the window are null.
        /// </summary>
        /// <param name="mode">The OpenTK.Graphics.GraphicsMode of the GraphicsContext.</param>
        /// <param name="window">The OpenTK.Platform.IWindowInfo to attach the GraphicsContext to.</param>
        /// <param name="shareContext">The GraphicsContext to share resources with, or null for explicit non-sharing.</param>
        /// <param name="major">The major version of the new GraphicsContext.</param>
        /// <param name="minor">The minor version of the new GraphicsContext.</param>
        /// <param name="flags">The GraphicsContextFlags for the GraphicsContext.</param>
        /// <remarks>
        /// Different hardware supports different flags, major and minor versions. Invalid parameters will be silently ignored.
        /// </remarks>
        public static GraphicsContext Create(GraphicsMode mode, IWindowInfo window, IGraphicsContext shareContext, int major, int minor, GraphicsContextFlags flags)
        {
            bool designMode = false;
            if (mode == null && window == null)
            {
                designMode = true;
            }
            else if (mode == null)
            {
                throw new ArgumentNullException("mode", "Must be a valid GraphicsMode.");
            }
            else if (window == null)
            {
                throw new ArgumentNullException("window", "Must point to a valid window.");
            }

            // Silently ignore invalid major and minor versions.
            if (major <= 0)
            {
                major = 1;
            }
            if (minor < 0)
            {
                minor = 0;
            }

            // Angle needs an embedded context
            const GraphicsContextFlags useAngleFlag = GraphicsContextFlags.Angle
                                                      | GraphicsContextFlags.AngleD3D9
                                                      | GraphicsContextFlags.AngleD3D11
                                                      | GraphicsContextFlags.AngleOpenGL;
            var useAngle = false;
            if ((flags & useAngleFlag) != 0)
            {
                flags |= GraphicsContextFlags.Embedded;
                useAngle = true;
            }

            IGraphicsContext implementation;
            IBindingsContext bindingContext;
            GraphicsContext.GetCurrentContextDelegate getCurrentContext;
            Debug.Print("Creating GraphicsContext.");
            try
            {
                Debug.Indent();
                Debug.Print("GraphicsMode: {0}", mode);
                Debug.Print("IWindowInfo: {0}", window);
                Debug.Print("GraphicsContextFlags: {0}", flags);
                Debug.Print("Requested version: {0}.{1}", major, minor);

                IPlatformFactory factory = null;
                switch ((flags & GraphicsContextFlags.Embedded) == GraphicsContextFlags.Embedded)
                {
                    case false:
                        factory = Factory.Default;
                        break;
                    case true:
                        factory = useAngle ? Factory.Angle : Factory.Embedded;
                        break;
                }

                // Note: this approach does not allow us to mix native and EGL contexts in the same process.
                // This should not be a problem, as this use-case is not interesting for regular applications.
                // Note 2: some platforms may not support a direct way of getting the current context
                // (this happens e.g. with DummyGLContext). In that case, we use a slow fallback which
                // iterates through all known contexts and checks if any is current (check GetCurrentContext
                // declaration).
                getCurrentContext = factory.CreateGetCurrentGraphicsContext();
                implementation = factory.CreateGLContext(mode, window, shareContext, GraphicsContext.DirectRendering, major, minor, flags);
                bindingContext = (IBindingsContext)implementation;
                var context = new GraphicsContext(implementation, bindingContext, getCurrentContext);
                factory.RegisterResource(context);
                return context;
            }
            finally
            {
                Debug.Unindent();
            }
        }
    }
}
