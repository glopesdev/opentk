using System;
using System.Runtime.InteropServices;

namespace OpenTK.Window.GraphicsLibrary
{
    internal static unsafe class GLFWNative
    {
        private const string LibraryName = "glfw3.dll";

        public const int GLFW_TRUE = 1;
        public const int GLFW_FALSE = 0;

#if NETCOREAPP
        static GLFWNative()
        {
            // Register DllImport resolver so that the correct dynamic library is loaded on all platforms.
            // On net472, we rely on Mono's DllMap for this. See the .dll.config file.
            NativeLibrary.SetDllImportResolver(typeof(GLFWNative).Assembly, (name, assembly, path) =>
            {
                if (name != "glfw3.dll")
                {
                    return IntPtr.Zero;
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return NativeLibrary.Load("libglfw.so.3", assembly, path);
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return NativeLibrary.Load("libglfw.3.dylib", assembly, path);
                }

                return IntPtr.Zero;
            });
        }
#endif

        [DllImport(LibraryName)]
        public static extern int glfwInit();

        [DllImport(LibraryName)]
        public static extern void glfwTerminate();

        [DllImport(LibraryName)]
        public static extern void glfwInitHint(int hint, int value);

        [DllImport(LibraryName)]
        public static extern void glfwGetVersion(int* major, int* minor, int* revision);

        [DllImport(LibraryName)]
        public static extern byte* glfwGetVersionString();

        [DllImport(LibraryName)]
        public static extern ErrorCode glfwGetError(byte** description);

        [DllImport(LibraryName)]
        public static extern MonitorHandle** glfwGetMonitors(int* count);

        [DllImport(LibraryName)]
        public static extern void glfwGetMonitorPos(MonitorHandle* monitor, int* x, int* y);

        [DllImport(LibraryName)]
        public static extern void glfwGetMonitorPhysicalSize(MonitorHandle* monitor, int* width, int* height);

        [DllImport(LibraryName)]
        public static extern void glfwGetMonitorContentScale(MonitorHandle* monitor, float* xscale, float* yscale);

        [DllImport(LibraryName)]
        public static extern byte* glfwGetMonitorName(MonitorHandle* monitor);

        [DllImport(LibraryName)]
        public static extern void glfwSetMonitorUserPointer(MonitorHandle* monitor, void* pointer);

        [DllImport(LibraryName)]
        public static extern void* glfwGetMonitorUserPointer(MonitorHandle* monitor);

        [DllImport(LibraryName)]
        public static extern VideoMode* glfwGetVideoModes(MonitorHandle* monitor, int* count);

        [DllImport(LibraryName)]
        public static extern void glfwSetGamma(MonitorHandle* monitor, float gamma);

        [DllImport(LibraryName)]
        public static extern GammaRamp* glfwGetGammaRamp(MonitorHandle* monitor);

        [DllImport(LibraryName)]
        public static extern void glfwSetGammaRamp(MonitorHandle* monitor, GammaRamp* ramp);

        [DllImport(LibraryName)]
        public static extern void glfwDefaultWindowHints();

        [DllImport(LibraryName)]
        public static extern void glfwWindowHintString(int hint, byte* value);

        [DllImport(LibraryName)]
        public static extern void glfwSetWindowSizeLimits(WindowHandle* window, int minwidth, int minheight, int maxwidth, int maxheight);

        [DllImport(LibraryName)]
        public static extern void glfwSetWindowAspectRatio(WindowHandle* window, int numer, int denom);

        [DllImport(LibraryName)]
        public static extern void glfwGetWindowFrameSize(WindowHandle* window, int* left, int* top, int* right, int* bottom);

        [DllImport(LibraryName)]
        public static extern void glfwGetWindowContentScale(WindowHandle* window, float* xscale, float* yscale);

        [DllImport(LibraryName)]
        public static extern float glfwGetWindowOpacity(WindowHandle* window);

        [DllImport(LibraryName)]
        public static extern void glfwSetWindowOpacity(WindowHandle* window, float opacity);

        [DllImport(LibraryName)]
        public static extern void glfwRequestWindowAttention(WindowHandle* window);

        [DllImport(LibraryName)]
        public static extern void glfwSetWindowAttrib(WindowHandle* window, WindowAttributeSetter attrib, int value);

        [DllImport(LibraryName)]
        public static extern int glfwRawMouseMotionSupported();

        [DllImport(LibraryName)]
        public static extern byte* glfwGetKeyName(Keys key, int scancode);

        [DllImport(LibraryName)]
        public static extern int glfwGetKeyScancode(Keys key);

        [DllImport(LibraryName)]
        public static extern InputAction glfwGetKey(WindowHandle* window, Keys key);

        [DllImport(LibraryName)]
        public static extern InputAction glfwGetMouseButton(WindowHandle* window, MouseButton button);

        [DllImport(LibraryName)]
        public static extern void glfwGetCursorPos(WindowHandle* window, double* xpos, double* ypos);

        [DllImport(LibraryName)]
        public static extern void glfwSetCursorPos(WindowHandle* window, double xpos, double ypos);

        [DllImport(LibraryName)]
        public static extern Cursor* glfwCreateCursor(Image* image, int xhot, int yhot);

        [DllImport(LibraryName)]
        public static extern Cursor* glfwCreateStandardCursor(CursorShape shape);

        [DllImport(LibraryName)]
        public static extern void glfwDestroyCursor(Cursor* cursor);

        [DllImport(LibraryName)]
        public static extern void glfwSetCursor(WindowHandle* window, Cursor* cursor);

        [DllImport(LibraryName)]
        public static extern int glfwJoystickPresent(int jid);

        [DllImport(LibraryName)]
        public static extern float* glfwGetJoystickAxes(int jid, int* count);

        [DllImport(LibraryName)]
        public static extern InputAction* glfwGetJoystickButtons(int jid, int* count);

        [DllImport(LibraryName)]
        public static extern JoystickHats* glfwGetJoystickHats(int jid, int* count);

        [DllImport(LibraryName)]
        public static extern byte* glfwGetJoystickName(int jid);

        [DllImport(LibraryName)]
        public static extern byte* glfwGetJoystickGUID(int jid);

        [DllImport(LibraryName)]
        public static extern void glfwSetJoystickUserPointer(int jid, void* ptr);

        [DllImport(LibraryName)]
        public static extern void* glfwGetJoystickUserPointer(int jid);

        [DllImport(LibraryName)]
        public static extern int glfwJoystickIsGamepad(int jid);

        [DllImport(LibraryName)]
        public static extern int glfwUpdateGamepadMappings(byte* newMapping);

        [DllImport(LibraryName)]
        public static extern byte* glfwGetGamepadName(int jid);

        [DllImport(LibraryName)]
        public static extern int glfwGetGamepadState(int jid, GamepadState* state);

        [DllImport(LibraryName)]
        public static extern double glfwGetTime();

        [DllImport(LibraryName)]
        public static extern void glfwSetTime(double time);

        [DllImport(LibraryName)]
        public static extern long glfwGetTimerValue();

        [DllImport(LibraryName)]
        public static extern long glfwGetTimerFrequency();

        [DllImport(LibraryName)]
        public static extern WindowHandle* glfwGetCurrentContext();

        [DllImport(LibraryName)]
        public static extern void glfwSwapBuffers(WindowHandle* window);

        [DllImport(LibraryName)]
        public static extern int glfwExtensionSupported(byte* extensionName);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwGetProcAddress(byte* procame);

        [DllImport(LibraryName)]
        public static extern WindowHandle* glfwCreateWindow(int width, int height, byte* title, MonitorHandle* monitor, WindowHandle* share);

        [DllImport(LibraryName)]
        public static extern MonitorHandle* glfwGetPrimaryMonitor();

        [DllImport(LibraryName)]
        public static extern void glfwDestroyWindow(WindowHandle* window);

        [DllImport(LibraryName)]
        public static extern void glfwFocusWindow(WindowHandle* window);

        [DllImport(LibraryName)]
        public static extern void glfwGetFramebufferSize(WindowHandle* window, int* width, int* height);

        [DllImport(LibraryName)]
        public static extern CursorModeValue glfwGetInputMode(WindowHandle* window, CursorStateAttribute mode);

        [DllImport(LibraryName)]
        public static extern int glfwGetInputMode(WindowHandle* window, StickyAttributes mode);

        [DllImport(LibraryName)]
        public static extern void glfwRestoreWindow(WindowHandle* window);

        [DllImport(LibraryName)]
        public static extern VideoMode* glfwGetVideoMode(MonitorHandle* monitor);

        [DllImport(LibraryName)]
        public static extern int glfwGetWindowAttrib(WindowHandle* window, WindowAttributeGetter attribute);

        [DllImport(LibraryName)]
        public static extern void glfwGetWindowSize(WindowHandle* window, int* width, int* height);

        [DllImport(LibraryName)]
        public static extern void glfwGetWindowPos(WindowHandle* window, int* x, int* y);

        [DllImport(LibraryName)]
        public static extern MonitorHandle* glfwGetWindowMonitor(WindowHandle* window);

        [DllImport(LibraryName)]
        public static extern void glfwHideWindow(WindowHandle* window);

        [DllImport(LibraryName)]
        public static extern void glfwIconifyWindow(WindowHandle* window);

        [DllImport(LibraryName)]
        public static extern void glfwMakeContextCurrent(WindowHandle* window);

        [DllImport(LibraryName)]
        public static extern void glfwMaximizeWindow(WindowHandle* window);

        [DllImport(LibraryName)]
        public static extern void glfwPollEvents();

        [DllImport(LibraryName)]
        public static extern void glfwPostEmptyEvent();

        [DllImport(LibraryName)]
        public static extern void glfwWindowHint(WindowHintInt hint, int value);

        [DllImport(LibraryName)]
        public static extern void glfwWindowHint(WindowHintBool hint, int value);

        [DllImport(LibraryName)]
        public static extern void glfwWindowHint(WindowHintClientApi hint, ClientApi value);

        [DllImport(LibraryName)]
        public static extern void glfwWindowHint(WindowHintReleaseBehavior hint, ReleaseBehavior value);

        [DllImport(LibraryName)]
        public static extern void glfwWindowHint(WindowHintContextApi hint, ContextApi value);

        [DllImport(LibraryName)]
        public static extern void glfwWindowHint(WindowHintRobustness hint, Robustness value);

        [DllImport(LibraryName)]
        public static extern void glfwWindowHint(WindowHintOpenGlProfile hint, OpenGlProfile value);

        [DllImport(LibraryName)]
        public static extern int glfwWindowShouldClose(WindowHandle* window);

        [DllImport(LibraryName)]
        public static extern void glfwSetWindowUserPointer(WindowHandle* window, void* pointer);

        [DllImport(LibraryName)]
        public static extern void* glfwGetWindowUserPointer(WindowHandle* window);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetCharCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetCharModsCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetCursorEnterCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetCursorPosCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetDropCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetErrorCallback(IntPtr callback);

        [DllImport(LibraryName)]
        public static extern void glfwSetInputMode(WindowHandle* window, CursorStateAttribute mode, CursorModeValue value);

        [DllImport(LibraryName)]
        public static extern void glfwSetInputMode(WindowHandle* window, StickyAttributes mode, int value);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetJoystickCallback(IntPtr callback);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetKeyCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetScrollCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetMonitorCallback(IntPtr callback);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetMouseButtonCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetWindowCloseCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetWindowFocusCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern void glfwSetWindowIcon(WindowHandle* window, int count, Image* images);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetWindowIconifyCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetWindowMaximizeCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetFramebufferSizeCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetWindowContentScaleCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern void glfwSetWindowTitle(WindowHandle* window, byte* title);

        [DllImport(LibraryName)]
        public static extern void glfwShowWindow(WindowHandle* window);

        [DllImport(LibraryName)]
        public static extern void glfwSetWindowSize(WindowHandle* window, int width, int height);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetWindowSizeCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern void glfwSetWindowShouldClose(WindowHandle* window, int value);

        [DllImport(LibraryName)]
        public static extern void glfwSetWindowMonitor(WindowHandle* window, MonitorHandle* monitor, int x, int y, int width, int height, int refreshRate);

        [DllImport(LibraryName)]
        public static extern void glfwSetWindowPos(WindowHandle* window, int x, int y);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetWindowPosCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwSetWindowRefreshCallback(WindowHandle* window, IntPtr callback);

        [DllImport(LibraryName)]
        public static extern void glfwSwapInterval(int interval);

        [DllImport(LibraryName)]
        public static extern void glfwWaitEvents();

        [DllImport(LibraryName)]
        public static extern void glfwWaitEventsTimeout(double timeout);

        [DllImport(LibraryName)]
        public static extern byte* glfwGetClipboardString(WindowHandle* window);

        [DllImport(LibraryName)]
        public static extern void glfwSetClipboardString(WindowHandle* window, byte* data);

        [DllImport(LibraryName)]
        public static extern int glfwVulkanSupported();

        [DllImport(LibraryName)]
        public static extern byte** glfwGetRequiredInstanceExtensions(uint* count);

        [DllImport(LibraryName)]
        public static extern IntPtr glfwGetInstanceProcAddress(VkHandle instance, byte* procName);

        [DllImport(LibraryName)]
        public static extern int glfwGetPhysicalDevicePresentationSupport(VkHandle instance, VkHandle device, int queueFamily);

        [DllImport(LibraryName)]
        public static extern int glfwCreateWindowSurface(VkHandle instance, WindowHandle* window, void* allocator, VkHandle surface);
    }
}
