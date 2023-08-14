using System;

namespace API.Utilities.Handlers
{
    public class GenerateHandler
    {
        public static string Nik(string? nik)
        {
            // Cek jika input nik adalah null atau mengandung karakter non-digit
            if (nik is null)
            {
                // Mengembalikan default NIK "111111" jika input nik adalah null atau kosong
                return "111111";
            }

            // Jika input nik adalah angka, tambahkan 1 ke angka tersebut
            var generatedNik = int.Parse(nik) + 1;

            return generatedNik.ToString();
        }
    }
}
