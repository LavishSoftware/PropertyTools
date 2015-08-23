// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataGridControlFactory.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Creates display and edit controls for the DataGrid.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Shapes;
    
    using PropertyTools.DataAnnotations;

    using HorizontalAlignment = System.Windows.HorizontalAlignment;

    /// <summary>
    /// Creates display and edit controls for the DataGrid.
    /// </summary>
    public class DataGridControlFactory : IDataGridControlFactory
    {
        /// <summary>
        /// Creates the display control with data binding.
        /// </summary>
        /// <param name="propertyDefinition">The property definition.</param>
        /// <param name="bindingPath">The binding path.</param>
        /// <returns>
        /// The control.
        /// </returns>
        public virtual FrameworkElement CreateDisplayControl(PropertyDefinition propertyDefinition, string bindingPath)
        {
            var control = propertyDefinition.CreateDisplayControl(bindingPath);
            if (control != null)
                return control;

            // check for ControlFactoriesAttribute on the property
            DataAnnotations.ControlFactoriesAttribute cfa = propertyDefinition.Descriptor.GetFirstAttributeOrDefault<DataAnnotations.ControlFactoriesAttribute>();
            if (cfa != null && !string.IsNullOrEmpty(cfa.DataGrid_DisplayFactoryMethod))
            {                
                var methodInfo = propertyDefinition.Descriptor.ComponentType.GetMethod(cfa.DataGrid_DisplayFactoryMethod);
                if (methodInfo!=null)
                    return (FrameworkElement)methodInfo.Invoke(null, new object[] { propertyDefinition, bindingPath });
            }

            // check for ControlFactoriesAttribute on the property's Type
            object[] cfas = propertyDefinition.PropertyType.GetCustomAttributes(typeof(DataAnnotations.ControlFactoriesAttribute), false);
            if (cfas!=null && cfas.Length>0)
            {
                cfa = (DataAnnotations.ControlFactoriesAttribute)cfas[0];
                if (cfa != null && !string.IsNullOrEmpty(cfa.DataGrid_DisplayFactoryMethod))
                {
                    var methodInfo = propertyDefinition.PropertyType.GetMethod(cfa.DataGrid_DisplayFactoryMethod);
                    if (methodInfo != null)
                        return (FrameworkElement)methodInfo.Invoke(null, new object[] { propertyDefinition, bindingPath });
                }
            }

            var propertyType = propertyDefinition.PropertyType;
            if (propertyType.Is(typeof(bool)))
            {
                return this.CreateCheckBoxControl(propertyDefinition, bindingPath);
            }

            if (propertyType.Is(typeof(Color)))
            {
                return this.CreateColorPreviewControl(propertyDefinition, bindingPath);
            }

            return this.CreateTextBlockControl(propertyDefinition, bindingPath);
        }

        /// <summary>
        /// Creates the edit control with data binding.
        /// </summary>
        /// <param name="propertyDefinition">The property definition.</param>
        /// <param name="bindingPath">The binding path.</param>
        /// <returns>
        /// The control.
        /// </returns>
        public virtual FrameworkElement CreateEditControl(PropertyDefinition propertyDefinition, string bindingPath)
        {
            var control = propertyDefinition.CreateEditControl(bindingPath);
            if (control != null)
                return control;

            // check for ControlFactoriesAttribute on the property
            DataAnnotations.ControlFactoriesAttribute cfa = propertyDefinition.Descriptor.GetFirstAttributeOrDefault<DataAnnotations.ControlFactoriesAttribute>();
            if (cfa != null && !string.IsNullOrEmpty(cfa.DataGrid_EditorFactoryMethod))
            {
                var methodInfo = propertyDefinition.Descriptor.ComponentType.GetMethod(cfa.DataGrid_EditorFactoryMethod);
                if (methodInfo != null)
                    return (FrameworkElement)methodInfo.Invoke(null, new object[] { propertyDefinition, bindingPath });
            }

            // check for ControlFactoriesAttribute on the property's Type
            object[] cfas = propertyDefinition.PropertyType.GetCustomAttributes(typeof(DataAnnotations.ControlFactoriesAttribute), false);
            if (cfas != null && cfas.Length > 0)
            {
                cfa = (DataAnnotations.ControlFactoriesAttribute)cfas[0];
                if (cfa != null && !string.IsNullOrEmpty(cfa.DataGrid_EditorFactoryMethod))
                {
                    var methodInfo = propertyDefinition.PropertyType.GetMethod(cfa.DataGrid_EditorFactoryMethod);
                    if (methodInfo != null)
                        return (FrameworkElement)methodInfo.Invoke(null, new object[] { propertyDefinition, bindingPath });
                }
            }

            var propertyType = propertyDefinition.PropertyType;
            if (propertyDefinition.ItemsSourceProperty != null || propertyDefinition.ItemsSource != null)
            {
                return this.CreateComboBox(propertyDefinition, bindingPath);
            }

            if (propertyType == typeof(bool))
            {
                return null;
            }

            if (propertyType != null && propertyType.Is(typeof(Color)))
            {
                return this.CreateColorPickerControl(propertyDefinition, bindingPath);
            }

            return this.CreateTextBox(propertyDefinition, bindingPath);
        }

        /// <summary>
        /// Creates a check box control with data binding.
        /// </summary>
        /// <param name="propertyDefinition">The property definition.</param>
        /// <param name="bindingPath">The binding path.</param>
        /// <returns>
        /// A CheckBox.
        /// </returns>
        protected virtual FrameworkElement CreateCheckBoxControl(PropertyDefinition propertyDefinition, string bindingPath)
        {
            if (propertyDefinition.IsReadOnly)
            {
                var cm = new CheckMark
                             {
                                 VerticalAlignment = VerticalAlignment.Center,
                                 HorizontalAlignment = propertyDefinition.HorizontalAlignment,
                             };
                cm.SetBinding(CheckMark.ForegroundProperty, new Binding() { Mode = BindingMode.OneWay, ElementName = "PART_SheetGrid", Path = new PropertyPath("TemplatedParent.Foreground") });
                cm.SetBinding(CheckMark.IsCheckedProperty, propertyDefinition.CreateBinding(bindingPath));
                return cm;
            }

            var c = new CheckBox
                        {
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = propertyDefinition.HorizontalAlignment,
                            IsEnabled = !propertyDefinition.IsReadOnly,
                         Cursor = System.Windows.Input.Cursors.Arrow,
                        };

            c.SetBinding(CheckBox.ForegroundProperty, new Binding() { Mode = BindingMode.OneWay, ElementName = "PART_SheetGrid", Path = new PropertyPath("TemplatedParent.Foreground") });

            c.SetBinding(ToggleButton.IsCheckedProperty, propertyDefinition.CreateBinding(bindingPath));
            return c;
        }

        /// <summary>
        /// Creates a color picker control with data binding.
        /// </summary>
        /// <param name="propertyDefinition">The property definition.</param>
        /// <param name="bindingPath">The binding path.</param>
        /// <returns>
        /// A color picker.
        /// </returns>
        protected virtual FrameworkElement CreateColorPickerControl(PropertyDefinition propertyDefinition, string bindingPath)
        {
            var c = new ColorPicker
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Focusable = false
                };
            c.SetBinding(ColorPicker.SelectedColorProperty, propertyDefinition.CreateBinding(bindingPath));
            return c;
        }

        /// <summary>
        /// Creates a color preview control with data binding.
        /// </summary>
        /// <param name="propertyDefinition">The property definition.</param>
        /// <param name="bindingPath">The binding path.</param>
        /// <returns>
        /// A preview control.
        /// </returns>
        protected virtual FrameworkElement CreateColorPreviewControl(PropertyDefinition propertyDefinition, string bindingPath)
        {
            var c = new Rectangle
                        {
                            Stroke = Brushes.Black,
                            StrokeThickness = 1,
                            Width = 12,
                            Height = 12,
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center
                        };

            var binding = propertyDefinition.CreateBinding(bindingPath);
            binding.Converter = new ColorToBrushConverter();
            c.SetBinding(Shape.FillProperty, binding);
            return c;
        }

        /// <summary>
        /// Creates a combo box with data binding.
        /// </summary>
        /// <param name="propertyDefinition">The property definition.</param>
        /// <param name="bindingPath">The binding path.</param>
        /// <returns>
        /// A ComboBox.
        /// </returns>
        protected virtual FrameworkElement CreateComboBox(PropertyDefinition propertyDefinition, string bindingPath)
        {
            var c = new ComboBox { IsEditable = propertyDefinition.IsEditable, Focusable = true, Margin = new Thickness(0, 0, -1, -1) };
            c.Cursor = System.Windows.Input.Cursors.Arrow;
            if (propertyDefinition.ItemsSource != null)
            {
                c.ItemsSource = propertyDefinition.ItemsSource;
            }
            else
            {
                if (propertyDefinition.ItemsSourceProperty != null)
                {
                    var itemsSourceBinding = new Binding(propertyDefinition.ItemsSourceProperty);
                    c.SetBinding(ItemsControl.ItemsSourceProperty, itemsSourceBinding);
                }
            }

            c.Loaded += (s, e) => { ((Popup)c.Template.FindName("PART_Popup", c)).PopupAnimation = PopupAnimation.None; };

            c.DropDownClosed += (s, e) => FocusParentDataGrid(c);
            var binding = propertyDefinition.CreateBinding(bindingPath);
            binding.NotifyOnSourceUpdated = true;
            c.SetBinding(propertyDefinition.IsEditable ? ComboBox.TextProperty : Selector.SelectedValueProperty, binding);

            var selectedValuePathAttribute = propertyDefinition.Descriptor.GetFirstAttributeOrDefault<PropertyTools.DataAnnotations.SelectedValuePathAttribute>();
            if (selectedValuePathAttribute != null)
            {
                c.SelectedValuePath = selectedValuePathAttribute.Path;
            }
            
            return c;
        }

        /// <summary>
        /// Creates a text block control with data binding.
        /// </summary>
        /// <param name="propertyDefinition">The property definition.</param>
        /// <param name="bindingPath">The binding path.</param>
        /// <returns>
        /// A TextBlock.
        /// </returns>
        protected virtual FrameworkElement CreateTextBlockControl(PropertyDefinition propertyDefinition, string bindingPath)
        {
            var tb = new TextBlock
            {
                HorizontalAlignment = propertyDefinition.HorizontalAlignment,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(4, 0, 4, 0),
            };

            tb.SetBinding(TextBlock.ForegroundProperty, new Binding() { Mode = BindingMode.OneWay, ElementName = "PART_SheetGrid", Path = new PropertyPath("TemplatedParent.Foreground") });
            tb.SetBinding(TextBlock.TextProperty, propertyDefinition.CreateOneWayBinding(bindingPath));
            return tb;
        }

        /// <summary>
        /// Creates a text box with data binding.
        /// </summary>
        /// <param name="propertyDefinition">The property definition.</param>
        /// <param name="bindingPath">The binding path.</param>
        /// <returns>A TextBox.</returns>
        protected virtual FrameworkElement CreateTextBox(PropertyDefinition propertyDefinition, string bindingPath)
        {
            var tb = new TextBox
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = propertyDefinition.HorizontalAlignment,
                MaxLength = propertyDefinition.MaxLength,
                BorderThickness = new Thickness(0),
                Margin = new Thickness(1, 1, 0, 0),
            };

            tb.SetBinding(TextBox.TextProperty, propertyDefinition.CreateBinding(bindingPath));
            return tb;
        }

        /// <summary>
        /// Focuses on the parent data grid.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject" />.</param>
        private static void FocusParentDataGrid(DependencyObject obj)
        {
            var parent = VisualTreeHelper.GetParent(obj);
            while (parent != null && !(parent is DataGrid))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            var u = parent as UIElement;
            if (u != null)
            {
                u.Focus();
            }
        }
    }
}
