using AnanasMVCWebApp.Models;

namespace AnanasMVCWebApp.Utilities.ObserverPattern {
    public interface IOrderNotifier {
        // Attach an order observer to the subject.
        public void Attach(IOrderObserver observer);

        // Detach an order observer from the subject.
        public void Detach(IOrderObserver observer);

        // Notify all order observers about an event.
        public void Notify(Order order);
    }
}
