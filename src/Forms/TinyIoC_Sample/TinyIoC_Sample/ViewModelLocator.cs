using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using TinyIoC;
using TinyIoC_Sample.Services;
using TinyIoC_Sample.ViewModels;
using Xamarin.Forms;

namespace TinyIoC_Sample
{
    public interface Ioc
    {
        T Resolve<T>() where T : class;
    }

    public class IocImpl : Ioc
    {
        public IocImpl()
        {
            Container = new TinyIoCContainer();
            RegisterServices();
        }

        public T Resolve<T>() where T : class
        {
            return Container.Resolve<T>();
        }

        private void RegisterServices()
        {
            Container.Register<Ioc>(this);
            Container.Register<IDatabaseService, DatabaseService>();
        }

        internal TinyIoCContainer Container { get; private set; }
    }

    public static class ViewModelLocator
    {
        private static readonly IocImpl _iocImpl = new IocImpl();

        public static readonly BindableProperty AutoWireViewModelProperty = BindableProperty.CreateAttached("AutoWireViewModel",
            typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        public static T Resolve<T>() where T : class
        {
            return _iocImpl.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (DesignMode.IsDesignModeEnabled)
            {
                return;
            }

            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            if (view.BindingContext == null)
            {
                var viewType = view.GetType();
                var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

                var viewModelType = Type.GetType(viewModelName);
                if (viewModelType == null)
                {
                    throw new InvalidOperationException($"Not found: {viewModelName}");
                }
                var viewModel = _iocImpl.Container.Resolve(viewModelType);
                view.BindingContext = viewModel;
            }

            var page = view as Page;
            if (page != null)
            {
                page.Appearing += (s, e) =>
                {
                    var model = view.BindingContext as BaseViewModel;
                    if (model != null)
                    {
                        model.Initialize();
                    }
                };

                page.Disappearing += (s, e) =>
                {
                    var model = view.BindingContext as BaseViewModel;
                    if (model != null)
                    {
                        // NOTE: Dispose call is not good in here, since page might be show and hide multiple times. Use navigator to dispose view model.
                    }
                };
            }
        }
    }
}
