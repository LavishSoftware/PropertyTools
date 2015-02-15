// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisplayMemberPathAttribute.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Specifies the display name for a property, event, or public void method which takes no arguments.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.DataAnnotations
{
    using System;

    /// <summary>
    /// Specifies the display member path for a combo box or list box
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayMemberPathAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayMemberPathAttribute"/> class.
        /// </summary>
        /// <param name="path">The display member path.</param>
        public DisplayMemberPathAttribute(string path)
        {
            this.Path = path;
        }

        /// <summary>
        /// Gets the display member path.
        /// </summary>
        /// <value>The display member path.</value>
        public string Path { get; private set; }
    }
}