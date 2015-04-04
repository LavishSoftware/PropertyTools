// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlFactoriesAttribute.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Specifies editor control factory methods for use in a DataGrid or Properties window
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.DataAnnotations
{
    using System;

    /// <summary>
    /// Specifies editor control factory methods for use in a DataGrid or Properties window
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
    public class ControlFactoriesAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlFactoriesAttribute"/> class.
        /// </summary>
        /// <param name="propertygrid_factorymethod" optional="True">Name of a static method matching IPropertyControlFactory.CreateControl</param>
        /// <param name="datagrid_displayfactorymethod" optional="True">Name of a static method matching IDataGridControlFactory.CreateDisplayControl</param>
        /// <param name="datagrid_editorfactorymethod" optional="True">Name of a static method matching IDataGridControlFactory.CreateEditControl</param>
        public ControlFactoriesAttribute(string propertygrid_factorymethod = null, string datagrid_displayfactorymethod = null, string datagrid_editorfactorymethod = null)
        {
            this.PropertyGrid_FactoryMethod = propertygrid_factorymethod;
            this.DataGrid_DisplayFactoryMethod = datagrid_displayfactorymethod;
            this.DataGrid_EditorFactoryMethod = datagrid_editorfactorymethod;
        }

        /// <summary>
        /// Gets or sets the name of a static method matching IPropertyControlFactory.CreateControl
        /// </summary>
        public string PropertyGrid_FactoryMethod { get; set; }

        /// <summary>
        /// Gets or sets the name of a static method matching IDataGridControlFactory.CreateDisplayControl
        /// </summary>
        public string DataGrid_DisplayFactoryMethod { get; set; }

        /// <summary>
        /// Gets or sets the name of a static method matching IDataGridControlFactory.CreateEditControl
        /// </summary>
        public string DataGrid_EditorFactoryMethod { get; set; }
    }
}