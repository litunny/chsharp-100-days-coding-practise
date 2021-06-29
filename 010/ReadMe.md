# Day 007 - Microsoft C# IObservable 

  ### What is C# IObservable?
  IObservable has a Subscribe method that must be implemented. It represents the registration of observers and returns an IDisposable object. As it returns an IDisposable it will be easier for us to release an observer from the subject properly. 

  ### Tuple<T, T, T> Example
  ```c#
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                var provider = new StockTrader();
                var i1 = new Investor();
                i1.Subscribe(provider);
                var i2 = new Investor();
                i2.Subscribe(provider);

                provider.Trade(new Stock());
                provider.Trade(new Stock());
                provider.Trade(null);
                provider.End();
            }
        }

        public class Stock
        {
            private string Symbol { get; set; }
            private decimal Price { get; set; }
        }

        public class Investor : IObserver<Stock>
        {
            public IDisposable unsubscriber;
            public virtual void Subscribe(IObservable<Stock> provider)
            {
                if (provider != null)
                {
                    unsubscriber = provider.Subscribe(this);
                }
            }
            public virtual void OnCompleted()
            {
                unsubscriber.Dispose();
            }
            public virtual void OnError(Exception e)
            {
            }
            public virtual void OnNext(Stock stock)
            {
            }
        }

        public class StockTrader : IObservable<Stock>
        {
            public StockTrader()
            {
                observers = new List<IObserver<Stock>>();
            }
            private IList<IObserver<Stock>> observers;
            public IDisposable Subscribe(IObserver<Stock> observer)
            {
                if (!observers.Contains(observer))
                {
                    observers.Add(observer);
                }
                return new Unsubscriber(observers, observer);
            }
            public class Unsubscriber : IDisposable
            {
                private IList<IObserver<Stock>> _observers;
                private IObserver<Stock> _observer;

                public Unsubscriber(IList<IObserver<Stock>> observers, IObserver<Stock> observer)
                {
                    _observers = observers;
                    _observer = observer;
                }

                public void Dispose()
                {
                    Dispose(true);
                }
                private bool _disposed = false;
                protected virtual void Dispose(bool disposing)
                {
                    if (_disposed)
                    {
                        return;
                    }
                    if (disposing)
                    {
                        if (_observer != null && _observers.Contains(_observer))
                        {
                            _observers.Remove(_observer);
                        }
                    }
                    _disposed = true;
                }
            }
            public void Trade(Stock stock)
            {
                foreach (var observer in observers)
                {
                    if (stock == null)
                    {
                        observer.OnError(new ArgumentNullException());
                    }
                    observer.OnNext(stock);
                }
            }
            public void End()
            {
                foreach (var observer in observers.ToArray())
                {
                    observer.OnCompleted();
                }
                observers.Clear();
            }
        }
    }

  ```
  ### References
  * https://dotnetcodr.com/2013/08/01/design-patterns-and-practices-in-net-the-observer-pattern