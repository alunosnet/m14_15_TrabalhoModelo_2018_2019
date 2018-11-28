using System.IO;

namespace M14_15_TrabalhoModelo_2018_2019
{
    class Utils
    {
        static public byte[] ImagemParaVetor(string ficheiro)
        {
            FileStream fs = new FileStream(ficheiro, FileMode.Open, FileAccess.Read);
            byte[] dados = new byte[fs.Length];
            fs.Read(dados, 0, (int)fs.Length);
            fs.Close();
            return dados;
        }
        static public void VetorParaImagem(byte[] imagem, string ficheiro)
        {
            FileStream fs = new FileStream(ficheiro, FileMode.Create, FileAccess.Write);
            fs.Write(imagem, 0, imagem.GetUpperBound(0));
            fs.Close();
        }

    }
}
