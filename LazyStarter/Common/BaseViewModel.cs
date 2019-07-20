
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace LazyStarter
{
    [Serializable]
    public abstract class BaseViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        #region// IDataErrorInfo

        public virtual string Error
        {
            get
            {
                return null;
            }
        }

        public virtual string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        protected virtual string Validate(string propertyName)
        {
            return null;
        }

        #endregion


        #region// INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Уведомить о изменении свойства
        /// </summary>
        protected void NotifyPropertyChanged<T>(Expression<Func<T>> action)
        {
            string propertyName = GetPropertyName(action);
            NotifyPropertyChanged(propertyName);
        }

        /// <summary>
        /// Получить имя свойства из лябда-выражения
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        protected static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            string propertyName = expression.Member.Name;
            return propertyName;
        }


        /// <summary>
        /// Получить имя свойства из класса
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        protected static string GetPropertyName<TEntity>(Expression<Func<TEntity, object>> property)
        {
            var convertExpression = property.Body as UnaryExpression;
            if (convertExpression != null)
            {
                return ((MemberExpression)convertExpression.Operand).Member.Name;
            }

            return ((MemberExpression)property.Body).Member.Name;
        }


        /// <summary>
        /// Уведомить о изменении свойства
        /// </summary>
        /// <param name="propertyName">Имя свойства</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        protected void WithClient<T>(T proxy, Action<T> codeToExecute)
        {
            codeToExecute.Invoke(proxy);

            IDisposable disposableClient = proxy as IDisposable;
            if (disposableClient != null)
                disposableClient.Dispose();
        }

        #endregion
    }
}