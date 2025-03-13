class Program
{
    static void Main()
    {
        Console.WriteLine("Kart türünü seçin (Visa, MasterCard, AmEx, Discover): ");
        string cardType = Console.ReadLine().ToLower();

        string binPrefix = cardType switch
        {
            "visa" => "453201",        // Visa prefix (BIN)
            "mastercard" => "522300",  // MasterCard prefix (BIN)
            "amex" => "374245",        // American Express prefix (BIN)
            "discover" => "601151",    // Discover prefix (BIN)
            _ => throw new ArgumentException("Geçersiz kart türü")
        };

        string generatedCardNumber = LuhnAlgorithm.LuhnAlgorithm.GenerateValidCardNumber(binPrefix);
        Console.WriteLine($"Otomatik oluşturulan geçerli {cardType} kredi kartı numarası: ");
        Console.WriteLine(generatedCardNumber);

        Console.WriteLine("Doğrulamak için bir kart numarası girin:");
        string inputCard = Console.ReadLine();
        bool isValid = LuhnAlgorithm.LuhnAlgorithm.ValidateCardNumber(inputCard);

        Console.WriteLine(isValid ? "Kart numarası geçerli!" : "Kart numarası geçersiz!");
    }
}
