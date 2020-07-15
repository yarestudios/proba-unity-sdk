namespace Proba
{
    public class Engine
    {
        public delegate void OnReady();

        /// <summary>
        /// Initialize the Proba SDK with the Game ID
        /// </summary>
        public static void Initialize(OnReady ready)
        {
            // The only thing we do is load the configuration
            // from the resources folder. This will give
            // us everything we need for now
            Configuration.Load();

            // Make sure we initialize the player data only
            // once. Even when we are "sure" we will call this once
            if (!Player.IsLoaded()) {
                Player.Initialize((r) => {
                    // Ready to use the player data
                    ready();
                });
            } else {
                // If we already initialized the engine and the player
                // then we are ready to continue. This is useful when
                // we come back to the init screen after a while
                ready();
            }
        }
    }
}
