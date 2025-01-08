namespace ProductApi.UtilityClasses
{
    public class InvoiceValidate
    {
        public InvoiceValidate() { }

        public bool ValidateProduct(string product)
        {
            return !string.IsNullOrEmpty(product);
        }

        public bool ValidateQuantity(int quantity)
        {
            return quantity > 0;
        }

        public bool ValidateUnitPrice(double unitPrice)
        {
            return unitPrice > 0;
        }

        public bool ValidateDiscount(double unitPrice, double discount, int quantity)
        {
            return discount >= 0 && (discount <= unitPrice * quantity);
        }
    }
}
