using LaundryApp.Data;
using LaundryApp.Model;
using LaundryApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryApp.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _db;

        public PaymentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreatePayment(Payment payment)
        {
            payment.PaymentId = GetPaymentsForCreate().Count + 1;
            payment.Status = true;
            _db.Payments.Add(payment);
            return Save();
        }

        public bool DeletePayment(Payment payment)
        {
            payment.Status = false;
            _db.Payments.Update(payment);
            return Save();
        }

        public Payment GetPayment(int paymentId)
        {
            return _db.Payments.FirstOrDefault(a => a.PaymentId == paymentId && a.Status);
        }

        public ICollection<Payment> GetPayments()
        {
            return _db.Payments.Where(a => a.Status).ToList();
        }

        public ICollection<Payment> GetPaymentsForCreate()
        {
            return _db.Payments.ToList();
        }

        public bool PaymentExist(int id)
        {
            return _db.Payments.Any(a => a.PaymentId == id && a.Status);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdatePayment(Payment payment)
        {
            payment.Status = true;
            _db.Payments.Update(payment);
            return Save();
        }
    }
}
