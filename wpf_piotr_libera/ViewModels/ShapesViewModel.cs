using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using wpf_piotr_libera.Models;
using wpf_piotr_libera.MVVM;

namespace wpf_piotr_libera.ViewModels
{
    public class ShapesViewModel : IViewModel, INotifyPropertyChanged
    {
        private ShapesModel ShapesModel { get; }
        private readonly CollectionViewSource collectionViewSource;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICollectionView Shapes { get; }

        private Shape selectedShape;

        public Shape SelectedShape
        {
            get { return selectedShape; }
            set
            {
                selectedShape = value;
                EditCommand.NotifyCanExecuteChanged();
                DeleteCommand.NotifyCanExecuteChanged();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedShape)));
            }
        }

        private string itemCount;
        public string ItemCount
        {
            get { return itemCount; }
            set { itemCount = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ItemCount")); }
        }

        private string filterText = "";
        public string FilterText
        {
            get
            {
                return filterText;
            }
            set
            {
                filterText = value;
                UpdateFilter();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterText)));
            }
        }
        private void UpdateFilter()
        {
            collectionViewSource.View.Refresh();
        }
        bool FilterShape(Shape shape)
        {
            if (shape.Text is null && shape.Color is null)
                return FilterText == "";
            if(shape.Text is null)
                return shape.Color.Contains(FilterText);
            if (shape.Color is null)
                return shape.Text.Contains(FilterText);
            return shape.Text.Contains(FilterText) || shape.Color.Contains(FilterText);
        }

        public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ItemCount = Shapes.Cast<object>().Count().ToString();
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Shape item in e.OldItems)
                {
                    //Removed items
                    item.PropertyChanged -= CollectionElementPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Shape item in e.NewItems)
                {
                    //Added items
                    item.PropertyChanged += CollectionElementPropertyChanged;
                }
            }
        }

        public void CollectionElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateFilter();
        }



        public Action Close { get; set; }
        private RelayCommand<object> addCommand;
        public RelayCommand<object> AddCommand => (addCommand = addCommand ?? new RelayCommand<object>(o => AddShape()));
        private RelayCommand<object> editCommand;
        public RelayCommand<object> EditCommand => (editCommand = editCommand ?? new RelayCommand<object>(o => EditShape(SelectedShape), o => SelectedShape != null));
        private RelayCommand<object> deleteCommand;
        public RelayCommand<object> DeleteCommand => (deleteCommand = deleteCommand ?? new RelayCommand<object>(o => DeleteShape(SelectedShape), o => SelectedShape != null));

        private RelayCommand<object> newWindowCommand;
        public RelayCommand<object> NewWindowCommand => (newWindowCommand = newWindowCommand ?? new RelayCommand<object>(o => NewWindow()));


        public ShapesViewModel(ShapesModel shapesModel)
        {
            ShapesModel = shapesModel;
            collectionViewSource = new CollectionViewSource
            {
                Source = ShapesModel.Shapes
            };
            collectionViewSource.View.Filter = (o) => FilterShape((Shape)o);
            Shapes = collectionViewSource.View;
            ItemCount = Shapes.Cast<object>().Count().ToString();
            Shapes.CollectionChanged += this.OnCollectionChanged;
            foreach (Shape item in Shapes)
            {
                item.PropertyChanged += CollectionElementPropertyChanged;
            }
        }

        public void NewWindow()
        {
            ShapesViewModel shapesViewModel = new ShapesViewModel(ShapesModel);
            ((App)Application.Current).WindowService.Show(shapesViewModel, Constants.mainWindowMinWidth, Constants.mainWindowMinHeight);
        }

        public void AddShape()
        {
            ShapeViewModel shapeViewModel = new ShapeViewModel(ShapesModel, null);
            ((App)Application.Current).WindowService.ShowDialog(shapeViewModel, Constants.dialogWindowMinWidth, Constants.dialogWindowMinHeight);
        }

        public void EditShape(Shape shape)
        {
            if (shape != null)
            {
                ShapeViewModel shapeViewModel = new ShapeViewModel(ShapesModel, shape);
                ((App)Application.Current).WindowService.ShowDialog(shapeViewModel);
            }
        }
        public void DeleteShape(Shape shape)
        {
            if (shape != null)
                ShapesModel.Shapes.Remove(shape);
        }
    }
}
