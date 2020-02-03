using System;
using System.Collections.Generic;
using System.Text;

namespace OpenTK
{
    /// <summary>
    /// Provides methods for querying available bindings from a native context.
    /// </summary>
    public interface IBindingsContext
    {
        /// <summary>
        /// Retrieves an unmanaged function pointer to the specified function on the specified bindings context.
        /// </summary>
        /// <param name="funcname">
        /// A <see cref="System.IntPtr"/> pointing to an ASCII-encoded string that defines the name of the function.
        /// </param>
        /// <returns>
        /// A <see cref="System.IntPtr"/> that contains the address of funcname or IntPtr.Zero,
        /// if the function is not supported by the drivers.
        /// </returns>
        /// <remarks>
        /// Note: some drivers are known to return non-zero values for unsupported functions.
        /// Typical values include 1 and 2 - inheritors are advised to check for and ignore these
        /// values.
        /// </remarks>
        IntPtr GetAddress(IntPtr funcname);
    }
}
