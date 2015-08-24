// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowsableAttribute.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Specifies whether a property or event should be displayed in a Properties window.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.DataAnnotations
{
    using System;

    /// <summary>
    /// Specifies whether a property or event should be displayed in a Properties window.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class BrowsableAttribute : Attribute
    {        
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowsableAttribute"/> class.
        /// </summary>
        /// <param name="browsable"><c>true</c> if a property or event can be browsed by PropertyGrid and DataGrid; otherwise, <c>false</c>. The default is <c>true</c>.</param>
        public BrowsableAttribute(bool browsable)
        {
            this.BrowsableByPropertyGrid = browsable;
            this.BrowsableByDataGrid = browsable;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowsableAttribute"/> class.
        /// </summary>
        /// <param name="browsableByPropertyGrid"><c>true</c> if a property or event can be browsed by PropertyGrid; otherwise, <c>false</c>. The default is <c>true</c>.</param>
        /// <param name="browsableByDataGrid"><c>true</c> if a property or event can be browsed by DataGrid; otherwise, <c>false</c>. The default is <c>true</c>.</param>
        public BrowsableAttribute(bool browsableByPropertyGrid, bool browsableByDataGrid)
        {
            this.BrowsableByPropertyGrid = browsableByPropertyGrid;
            this.BrowsableByDataGrid = browsableByDataGrid;
        }


        /// <summary>
        /// Gets a value indicating whether an object is browsable by PropertyGrid.
        /// </summary>
        /// <value><c>true</c> if the object is browsable by PropertyGrid; otherwise, <c>false</c>.</value>
        public bool BrowsableByPropertyGrid { get; private set; }

        /// <summary>
        /// Gets a value indicating whether an object is browsable by DataGrid.
        /// </summary>
        /// <value><c>true</c> if the object is browsable by DataGrid; otherwise, <c>false</c>.</value>
        public bool BrowsableByDataGrid { get; private set; }
    }
}