using Assemblify.Core;
using System;

namespace AssemblifyServer
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Debug.Log("Assemblify Server (Version: " + Properties.Resources.Version + ")");

            using (var game = new AssemblifyServerGame())
            {
                game.Run();
                game.Exiting += OnExit;
            }
        }

        private static void OnExit(object sender, EventArgs e)
        {
            Debug.StoreLog("debug.log", true);
        }
    }
#endif
}
