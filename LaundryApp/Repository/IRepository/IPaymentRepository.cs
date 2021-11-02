using LaundryApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryApp.Repository.IRepository
{
    public interface IPaymentRepository
    {
        ICollection<Payment> GetPayments();
        ICollection<Payment> GetPaymentsForCreate();
        Payment GetPayment(int paymentId);
        bool PaymentExist(int id);
        bool CreatePayment(Payment payment);
        bool UpdatePayment(Payment payment);
        bool DeletePayment(Payment payment);
        bool Save();
    }
}
