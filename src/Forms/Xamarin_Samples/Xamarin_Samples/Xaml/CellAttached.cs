using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;


namespace Xamarin_Samples.Xaml
{
    public class CellAttached
    {
        public static BindableProperty IsVisibleProperty =
          BindableProperty.CreateAttached(
              "IsVisible",
              typeof(bool),
              typeof(CellAttached),
              default(bool),
              defaultBindingMode: BindingMode.OneWay,
              propertyChanged: OnCollapsedChanged);

        private IList<TableSection> _hiddenSections = new List<TableSection>(); // TODO: unhide
        private IList<Cell> _hiddenCells = new List<Cell>();                    // TODO: unhide

        public static bool GetIsVisible(BindableObject target)
        {
            return (bool)target.GetValue(IsVisibleProperty);
        }

        public static void SetIsVisible(BindableObject target, bool value)
        {
            target.SetValue(IsVisibleProperty, value);
        }

        private static void OnCollapsedChanged(BindableObject sender, object oldValue, object newValue)
        {
            var view = sender as Cell;
            bool isVisible = (bool)newValue;
            if (view != null)
            {
                // the parent isn't available until the page has loaded.
                if (view.Parent == null)
                {
                    view.Appearing += (o, e) =>
                    {
                        ToggleViewCellCollapsedState(view, isVisible);
                    };
                }
                else
                {
                    ToggleViewCellCollapsedState(view, isVisible);
                }
            }
        }

        private static void ToggleViewCellCollapsedState(Cell cell, bool isVisible)
        {
            var table = (TableView)cell.Parent;
            TableSection container = FindContainingTableSection(table, cell);
            if (container != null)
            {
                if (!isVisible)
                {
                    // remove the cell from the section
                    container.Remove(cell);

                    // remove the section from the table if it's empty
                    if (container.Count == 0)
                    {
                        table.Root.Remove(container);
                    }
                }
            }
        }

        private static TableSection FindContainingTableSection(TableView table, Cell cell)
        {
            foreach (var section in table.Root)
            {
                foreach (var child in section)
                {
                    if (child == cell)
                    {
                        return section;
                    }
                }
            }

            return null;
        }
    }
}
