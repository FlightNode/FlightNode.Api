using System;
using System.Collections.Generic;

namespace FlightNode.Service.Navigation
{
    /// <summary>
    /// Represents a heirarchy that can be turned into a navigation tree.
    /// </summary>
    /// <example>
    /// <para>
    /// The following code creates a single parent item with children:
    /// </para>
    /// <code>
    /// var parent = new NavigationNode();
    /// var logout = new NavigationNode("Logout", "#/logout");
    /// var users = new NavigationNode("Manage", "#/users");
    /// parent.AddChild(users).AddChild(logout);
    /// 
    /// users.AddChild(new NavigationNode("Users", "#/users"));
    /// users.AddChild(new NavigationNode());
    /// users.AddChild(new NavigationNode("Work Days", "#/workdays"));
    /// users.AddChild(new NavigationNode("Work Types", "#/worktypes"));
    /// users.AddChild(new NavigationNode("Locations", "#/locations"));
    /// </code>
    /// <para>
    /// Result could be rendered in HTML as 
    /// </para>
    /// <code>
    /// * Logout
    /// * Manage
    /// ** Users
    /// ** ---------
    /// ** Work Days
    /// ** Work Types
    /// ** Locations
    /// </code>
    /// </example>
    public class NavigationNode
    {
        /// <summary>
        /// This node's immediate child nodes.
        /// </summary>
        public List<NavigationNode> Children { get; private set; }

        /// <summary>
        /// This node's <see cref="NavigationEntry"/> (the "parent" of the children).
        /// </summary>
        public NavigationEntry Entry { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="NavigationNode"/> without an entry.
        /// </summary>
        /// <remarks>
        /// Use at the top level of the navigation heirarchy.
        /// </remarks>
        public NavigationNode()
        {
            Children = new List<NavigationNode>();
        }

        /// <summary>
        /// Creates a new instance of <see cref="NavigationNode"/> with a parent <see cref="NavigationEntry"/> containing the supplied parameters values.
        /// </summary>
        /// <param name="title">Parent entry's title</param>
        /// <param name="path">Parent entry's path</param>
        public NavigationNode(string title, string path)
        {
            if (title == null)
            {
                throw new ArgumentNullException("title");
            }
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            Children = new List<NavigationNode>();
            Entry = new NavigationEntry
            {
                Title = title,
                Path = path
            };
        }

        /// <summary>
        /// Adds a new child <see cref="NavigationNode"/> entry to the collection.
        /// </summary>
        /// <param name="child">New instance of <see cref="NavigationNode"/></param>
        /// <returns>this</returns>
        public NavigationNode AddChild(NavigationNode child)
        {
            if (child == null)
            {
                throw new ArgumentNullException("child");
            }

            Children.Add(child);
            return this;
        }

    }

}