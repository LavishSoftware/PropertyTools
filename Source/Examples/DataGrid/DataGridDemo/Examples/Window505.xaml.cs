﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Window505.xaml.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   Interaction logic for Window505.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DataGridDemo
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Interaction logic for Window505.
    /// </summary>
    public partial class Window505
    {
        /// <summary>
        /// The shared items source. This makes it possible to open two windows and verify that property changes are working!
        /// </summary>
        private static readonly ObservableCollection<ObservableCollection<Fruit>> StaticItemsSource;

        /// <summary>
        /// The shared row headers items source.
        /// </summary>
        private static readonly ObservableCollection<string> StaticRowHeadersItemsSource;

        /// <summary>
        /// The shared column headers items source.
        /// </summary>
        private static readonly ObservableCollection<string> StaticColumnHeadersItemsSource;

        /// <summary>
        /// Initializes static members of the <see cref="Window505"/> class.
        /// </summary>
        static Window505()
        {
            StaticItemsSource = new ObservableCollection<ObservableCollection<Fruit>>
                                {
                                    new ObservableCollection<Fruit> { Fruit.Apple, Fruit.Banana, Fruit.Orange },
                                    new ObservableCollection<Fruit> { Fruit.Orange, Fruit.Banana, Fruit.Apple },
                                };
            StaticRowHeadersItemsSource = new ObservableCollection<string> { "Row I", "Row II" };
            StaticColumnHeadersItemsSource = new ObservableCollection<string> { "Fruit 1", "Fruit 2", "Fruit 3" };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Window505" /> class.
        /// </summary>
        public Window505()
        {
            this.InitializeComponent();
            this.CreateColumnHeader = i => "New column";
            this.DataContext = this;
        }

        /// <summary>
        /// Gets the items source.
        /// </summary>
        public ObservableCollection<ObservableCollection<Fruit>> ItemsSource
        {
            get
            {
                return StaticItemsSource;
            }
        }

        /// <summary>
        /// Gets the row headers items source.
        /// </summary>
        /// <value>
        /// The row headers source.
        /// </value>
        public ObservableCollection<string> RowHeadersItemsSource
        {
            get
            {
                return StaticRowHeadersItemsSource;
            }
        }

        /// <summary>
        /// Gets the column headers items source.
        /// </summary>
        /// <value>
        /// The column headers source.
        /// </value>
        public ObservableCollection<string> ColumnHeadersItemsSource
        {
            get
            {
                return StaticColumnHeadersItemsSource;
            }
        }

        /// <summary>
        /// Gets the create column header function.
        /// </summary>
        /// <value>The create column header.</value>
        public Func<int, object> CreateColumnHeader { get; private set; }
    }
}