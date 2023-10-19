using System.Security.Cryptography;
using System.Text;

namespace Controle_de_Contatos.Helper
{
    public static class Criptografia
    {
        public static string GerarHash(this string valor)//já que é um método estático, o this vai virar uma extensão da string. Ou seja, toda vez que tivermos uma string e darmos '.' ele vai aparecer o método "GerarHash"
        {
            var hash = SHA1.Create();
            var encondig = new ASCIIEncoding();
            var array = encondig.GetBytes(valor); //iremos pegar todos os bytes do valor passado como parâmetro. Por exemplo, se digitarmos '123456' ele vai pegar todos esses bytes do valor e vai transformar nesse array de bytes

            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();
            //o que vamos retornar aqui é uma string, então temos que montar uma StringBuilder para item por item do array de bytes, a gente possa ir montando o nosso hash

            foreach (var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
}
