using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuhnAlgorithm
{
    public class LuhnAlgorithm
    {
        public static bool ValidateCardNumber(string cardNumber)
        {
            int sum = 0; // Toplamı tutacak değişken
            bool alternate = false; // Her iki rakamda bir çarpma yapma kontrolü

            // Kart numarasını sağdan sola doğru döngü ile okuyoruz
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                // Karakteri sayıya dönüştürüyoruz
                int n = int.Parse(cardNumber[i].ToString());

                if (alternate) // Her iki rakamda bir işlem yapılacaksa
                {
                    n *= 2; // Sayıyı ikiyle çarpıyoruz
                    if (n > 9) // Eğer çarpma sonucu 9'dan büyükse
                    {
                        n -= 9; // 9 çıkarıyoruz
                    }
                }

                sum += n; // Sayıyı toplamla topluyoruz
                alternate = !alternate; // Sıradaki rakamda işlemi tersine çeviriyoruz
            }

            // Toplamın 10'a tam bölünüp bölünmediğini kontrol ediyoruz
            return (sum % 10 == 0);
        }


        public static string GenerateValidCardNumber(string bin)
        {
            Random random = new Random(); // Rastgele sayı üretmek için Random nesnesi
            List<int> digits = new List<int>(bin.Length + 9); // Kart numarasını tutacak liste

            // BIN numarasını (başlangıç kısmı) listeye ekliyoruz
            foreach (char c in bin)
            {
                digits.Add(int.Parse(c.ToString()));
            }

            // Rastgele rakamları ekliyoruz (son rakam hariç)
            for (int i = bin.Length; i < 15; i++)
            {
                digits.Add(random.Next(0, 10)); // 0 ile 9 arasında rastgele bir rakam
            }

            // Son kontrol basamağını hesaplıyoruz ve listeye ekliyoruz
            int checkDigit = CalculateCheckDigit(digits);
            digits.Add(checkDigit);

            // Listeyi string'e çevirip döndürüyoruz
            return string.Concat(digits);
        }


        private static int CalculateCheckDigit(List<int> digits)
        {
            int sum = 0; // Toplamı tutacak değişken
            bool alternate = true; // Her iki rakamda bir çarpma yapılacak mı?

            // Kart numarasındaki her rakamı sağdan sola doğru işliyoruz
            for (int i = digits.Count - 1; i >= 0; i--)
            {
                // Rakamı ikiyle çarpıyoruz veya olduğu gibi alıyoruz (her iki rakamda bir işlem)
                int digit = digits[i] * (alternate ? 2 : 1);

                // Eğer çarpma sonucu 9'dan büyükse, 9 çıkarıyoruz
                if (digit > 9)
                    digit -= 9;

                sum += digit; // Çarpılmış rakamı toplama ekliyoruz
                alternate = !alternate; // İşlemi tersine çeviriyoruz
            }

            // Toplamdan kontrol basamağını hesaplıyoruz
            int checkDigit = (10 - (sum % 10)) % 10;
            return checkDigit; // Hesaplanan kontrol basamağını döndürüyoruz
        }

    }
}
