﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Helpers
{
    public class NavigationHelper
    {

        public static string GetNavigateTo(NavigationViewItem item) =>
            (string)item.GetValue(NavigateToProperty);

        public static void SetNavigateTo(NavigationViewItem item, string value) =>
        item.SetValue(NavigateToProperty, value);

        public static readonly DependencyProperty NavigateToProperty =
            DependencyProperty.RegisterAttached("NavigateTo", typeof(string), typeof(NavigationHelper), new PropertyMetadata(null));
    }
}
