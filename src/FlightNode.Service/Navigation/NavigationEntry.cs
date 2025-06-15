namespace FlightNode.Service.Navigation
{

    /// <summary>
    /// Represents a single node in a navigation heirarchy 
    /// </summary>
    public class NavigationEntry
    {
        /// <summary>
        /// Title to display on the screen
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Navigation path (e.g. anchor href value)
        /// </summary>
        public string Path { get; set; }
    }
}