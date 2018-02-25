using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;           //DependencyProperty, RoutedEventHandler, ...
using System.Windows.Controls;  //ListView, GridViewColumn, ...
using System.ComponentModel;    //ListSortDirection, ...
using System.Windows.Data;      //Binding, CollectionViewSource, ...
using System.Reflection; 

namespace AdvertManageClient.Code
{
    /// <summary>
    /// ListViewHelper V7 for .NET 3.5+
    /// http://www.cnblogs.com/mgen/archive/2011/07/23/2114975.html
    /// </summary>
    public class ListViewHelper
    {
        #region 附加依赖属性定义

        //SortEnabled附加依赖属性
        public static readonly DependencyProperty SortEnabledProperty =
            DependencyProperty.RegisterAttached("SortEnabled", typeof(bool), typeof(ListViewHelper),
            new PropertyMetadata(OnSortEnabledChanged));

        //SortProperty附加依赖属性
        public static readonly DependencyProperty SortPropertyProperty =
            DependencyProperty.RegisterAttached("SortProperty", typeof(string), typeof(ListViewHelper),
            new PropertyMetadata(OnSortPropertyChanged));

        #endregion

        #region 依赖属性封装

        public static bool GetSortEnabled(ListView lv)
        {
            return (bool)lv.GetValue(SortEnabledProperty);
        }
        public static void SetSortEnabled(ListView lv, bool value)
        {
            lv.SetValue(SortEnabledProperty, value);
        }

        public static string GetSortProperty(GridViewColumn column)
        {
            if (column != null)
                return (string)column.GetValue(SortPropertyProperty);
            return null;
        }
        public static void SetSortProperty(GridViewColumn column, string propName)
        {
            if (column != null)
                column.SetValue(SortPropertyProperty, propName);
        }

        #endregion

        #region 属性改变事件

        static void OnSortEnabledChanged(DependencyObject dobj, DependencyPropertyChangedEventArgs e)
        {
            var listview = dobj as ListView;
            if (listview != null)
            {
                if ((bool)e.NewValue)
                    listview.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(OnGridViewColumnHeaderClicked));
                else
                    listview.RemoveHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(OnGridViewColumnHeaderClicked));
            }
        }

        static void OnSortPropertyChanged(DependencyObject dobj, DependencyPropertyChangedEventArgs e)
        {
            var column = dobj as GridViewColumn;
            var propName = (string)e.NewValue;

            var sortData = GetSortData(column);
            if (sortData == null)
                SetSortData(column, sortData = new SortData(propName));

            if (column.CellTemplate == null
                &&
                column.CellTemplateSelector == null)
            {
                var binding = new Binding(sortData.PropertyName);
                column.DisplayMemberBinding = binding;
            }
        }

        #endregion

        #region UI事件和辅助函数

        //GridViewColumn被点击事件
        static void OnGridViewColumnHeaderClicked(object sender, RoutedEventArgs e)
        {
            var header = e.OriginalSource as GridViewColumnHeader;
            var listview = sender as ListView;
            SortData sortData;

            if (listview != null
                && header != null
                && listview.ItemsSource != null
                && header.Column != null
                && (sortData = GetSortData(header.Column)) != null)
            {
                UpdateSortDescription(CollectionViewSource.GetDefaultView(listview.ItemsSource),
                    sortData);
            }
        }

        //更新SortDescription类型
        static void UpdateSortDescription(ICollectionView view, SortData sortData)
        {
            var listCollec = view as ListCollectionView;
            var srcType = view.SourceCollection.GetType();
            if (listCollec == null || (!srcType.IsGenericType && !srcType.IsArray))
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription(sortData.PropertyName, sortData.Direction));
            }
            else
            {
                Type targetType;
                if (srcType.IsArray)
                    targetType = srcType.GetElementType();
                else
                    targetType = srcType.GetGenericArguments()[0];
                listCollec.CustomSort = new ListItemComparer(sortData, targetType);
            }

            sortData.Direction = GetReverseDirection(sortData.Direction);
        }

        static ListSortDirection GetReverseDirection(ListSortDirection direction)
        {
            if (direction == ListSortDirection.Ascending)
                return ListSortDirection.Descending;
            return ListSortDirection.Ascending;
        }

        #endregion

        #region SortData

        public static readonly DependencyProperty SortDataProperty =
            DependencyProperty.RegisterAttached("SortData", typeof(SortData), typeof(ListViewHelper),
                new FrameworkPropertyMetadata((SortData)null));

        static SortData GetSortData(DependencyObject d)
        {
            if (d == null)
                return null;
            return (SortData)d.GetValue(SortDataProperty);
        }

        static void SetSortData(DependencyObject d, SortData value)
        {
            if (d == null)
                return;
            d.SetValue(SortDataProperty, value);
        }

        #endregion

    }

    public class SortData
    {
        const char REVERSE_CHAR = '#';
        public SortData(string raw)
        {
            Direction = ListSortDirection.Ascending;
            PropertyName = raw;

            if (raw.Length > 0 && raw[0] == REVERSE_CHAR)
            {
                Direction = ListSortDirection.Descending;
                PropertyName = raw.Substring(1);
            }
        }

        public string PropertyName { get; private set; }
        public ListSortDirection Direction { get; set; }
    }

    public class ListItemComparer : Comparer<object>
    {
        Func<object, object> del;

        public ListSortDirection Direction { get; set; }

        public ListItemComparer(SortData sortdata, Type targetType)
        {
            del = CompileInstanceProperty(targetType, sortdata.PropertyName);
            Direction = sortdata.Direction;
        }

        public override int Compare(object x, object y)
        {
            var res = Comparer<object>.Default.Compare(del(x), del(y));
            if (Direction == ListSortDirection.Ascending)
                return res;
            return res * -1;
        }

        Func<object, object> CompileInstanceProperty(Type targetType, string propName)
        {
            var propInfo = targetType.GetProperty(propName, BindingFlags.Instance | BindingFlags.Public);
            if (propInfo == null)
                throw new ArgumentException(String.Format("属性未找到：{0}", propName));

            var paExp = System.Linq.Expressions.Expression.Parameter(typeof(object));
            var convertedExp = System.Linq.Expressions.Expression.Convert(paExp, targetType);
            var propExp = System.Linq.Expressions.Expression.Property(convertedExp, propInfo);
            var retExp = System.Linq.Expressions.Expression.Convert(propExp, typeof(object));

            return System.Linq.Expressions.Expression.Lambda<Func<object, object>>(retExp, paExp).Compile();
        }
    }
}