using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoneClassLibrary
{
    public class Transaction
    {
        public decimal amount;
        public string type;
        public Card card;
        public int number;


        public Return validate()
        {
            //Codigos de erro:
            //LEGAL000 => Transação apovada!
            //LEGAL001 => Transação negada!
            //LEGAL002 => Saldo insuficiente!
            //LEGAL003 => Valor inválido!
            //LEGAL004 => Cartão bloqueado!
            //LEGAL005 => Erro no tamanho da senha!
            //LEGAL006 => Campos obrigatórios não informados!
            //LEGAL007 => Erro no tamanho do número do cartão!

            //verifica se algum campo obrigatorio nao foi informado
            if (amount == -1 ||
                String.IsNullOrEmpty(type) ||
                (type == "Crédito" && number == -1) ||
                String.IsNullOrEmpty(card.cardholderName) ||
                String.IsNullOrEmpty(card.number) ||
                String.IsNullOrEmpty(card.expirationDate) ||
                String.IsNullOrEmpty(card.cardBrand) ||
                (!card.hasPassword && String.IsNullOrEmpty(card.password)) ||
                String.IsNullOrEmpty(card.type)
                )
            {
                return new Return
                {
                    returnCode = "LEGAL006",
                    message = "Campos obrigatórios não informados!",
                    success = false
                };
            }

            //verifica se o valor e menor que 10 centavos
            if (amount < (decimal) 0.1)
            {
                return new Return
                {
                    returnCode = "LEGAL003",
                    message = "Valor inválido!",
                    success = false
                };
            }

            //verifica se a senha tem entre 4 e 6 caracteres
            if (!card.hasPassword && (card.password.Length < 4 || card.password.Length > 6))
            {
                return new Return
                {
                    returnCode = "LEGAL005",
                    message = "Erro no tamanho da senha!",
                    success = false
                };
            }

            //verifica se o numero do cartao tem entre 12 e 19 caracteres
            if (card.number.Length < 12 || card.number.Length > 19)
            {
                return new Return
                {
                    returnCode = "LEGAL007",
                    message = "Erro no tamanho do número do cartão!",
                    success = false
                };
            }

            //decidir aleatoriamente entre os demais codigos de retorno
            Random rnd = new Random();
            int code = rnd.Next(4);
            Return returnObj = new Return();

            switch (code)
            {
                case 0:
                    returnObj.returnCode = "LEGAL000";
                    returnObj.message = "Transação aprovada!";
                    break;
                case 1:
                    returnObj.returnCode = "LEGAL001";
                    returnObj.message = "Transação negada!";
                    break;
                case 2:
                    returnObj.returnCode = "LEGAL002";
                    returnObj.message = "Saldo insuficiente!";
                    break;
                case 3:
                    returnObj.returnCode = "LEGAL004";
                    returnObj.message = "Cartão bloqueado!";
                    break;
            }

            returnObj.success = true;
            return returnObj;
        }
    }

}
