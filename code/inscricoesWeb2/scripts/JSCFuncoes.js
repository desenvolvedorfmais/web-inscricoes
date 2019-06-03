/* ConverterMaiuscula   - Converte os caracteres digitados para seu equivalente maiúsculo. Usar onKeyPress="ConverterMaiuscula(this, event)"                                    */
/* ConverterMinuscula   - Converte os caracteres digitados para seu equivalente minúsculo. Usar onKeyPress="ConverterMinuscula(this, event)"                                    */
/* CPFCNPJMascarar      - Mascara o texto digitado no formato CPF ou CNPJ. Usar onKeyPress="return CPFCNPJMascarar(this, event)"                                                */
/* CPFMascarar          - Mascara o texto digitado no formato CPF. Usar onKeyPress="return CPFMascarar(this, event)"                                                            */
/* CNPJMascarar         - Mascara o texto digitado no formato CNPJ. Usar onKeyPress="return CNPJMascarar(this, event)"                                                          */
/* CEPMascarar          - Mascara o texto digitado no formato CEP. Usar onKeyPress="return CEPMascarar(this, event)"                                                            */ 
/* Mascarar             - Mascara o texto digitado no formato desejado, conforme máscara. Usar onKeyPress="return Mascarar(this, event, máscara)"                               */
/*                        Utilizar '9' para somente números ou 'A' para letras.                                                                                                 */
/* CPFValidar           - Valida o CPF. Usar no custom validator CPFValidar.                                                                                                    */
/* CNPJValidar          - Valida o CNPJ. Usar no custom validator CNPJValidar.                                                                                                  */
/* CPFCNPJValidar       - Valida o CPF/CNPJ. Usar no custom validator CPFCNPJValidar.                                                                                           */
/* CEPValidar           - Valida o CEP. Usar no custom validator CEPValidar.                                                                                                    */          
/* FoneValidar          - Valida o Fone. Usar no custom validator FoneValidar.                                                                                                  */          
/* MoedaMascarar        - Mascara o texto digitado no formato R$999.999.999,99. Usar onKeyPress="return MoedaMascarar(this,event)" e onkeyup="VerificarBackSpace(this,event)"   */  

function ConverterMaiuscula(DsParm, CdTecla)
{ 
    if (CdTecla.keyCode) 
    {
         var tecla = String.fromCharCode(CdTecla.keyCode).toUpperCase();
         CdTecla.keyCode = tecla.charCodeAt(0);
    }
    else if (CdTecla.which)
    {
        var tecla = String.fromCharCode(CdTecla.which).toUpperCase();
        CdTecla.which = tecla.charCodeAt(0);
    }
    else 
    {
        var tecla = String.fromCharCode(CdTecla.charCode).toUpperCase();
        CdTecla.which = tecla.charCodeAt(0);
    }    
}

function ConverterMinuscula(DsParm, CdTecla)
{ 
    if (CdTecla.keyCode) 
    {
        var tecla = String.fromCharCode(CdTecla.keyCode).toLowerCase();
        CdTecla.keyCode = tecla.charCodeAt(0);        
    }
    else if (CdTecla.which)
    {
        var tecla = String.fromCharCode(CdTecla.which).toLowerCase();
        CdTecla.which = tecla.charCodeAt(0);
    }
    else 
    {
        var tecla = String.fromCharCode(CdTecla.charCode).toLowerCase();
        CdTecla.which = tecla.charCodeAt(0);
    }    
}

function CPFCNPJMascarar(DsParm, CdTecla)
{
    if (CdTecla.keyCode){var tecla = CdTecla.keyCode;}
    else if (CdTecla.which){var tecla = CdTecla.which;}
    else {var tecla = CdTecla.charCode;}
    
    if ( tecla != 9 && tecla != 8 ){if (tecla < 48 || tecla > 57){return false;}}
    
    var cpjcnpj = '';
    cpfcnpj = DsParm.value.replace('.','').replace('.','').replace('-','').replace('/','')
    
    if(cpfcnpj.length <= 11)
    {
        CPFMascarar(DsParm, CdTecla);
    }
    else if (cpfcnpj.length < 14)
    {
        CNPJMascarar(DsParm, CdTecla);  
    }
    else {return false}
    
    return true;
} 


function CPFMascarar(DsParm, CdTecla)
{   
    if (CdTecla.keyCode){var tecla = CdTecla.keyCode;}
    else if (CdTecla.which){var tecla = CdTecla.which;}
    else {var tecla = CdTecla.charCode;}
    
    if ( tecla != 9 && tecla != 8 ){if (tecla < 48 || tecla > 57){return false;}}
    
    var cpf = '';
    var tamanho = 0;
    cpf = DsParm.value.replace('.','').replace('.','').replace('-','').replace('/','')
    tamanho = cpf.length + 1;
    
    if(cpf.length < 11)
    {
        if ( CdTecla != 9 && CdTecla != 8 ) 
        {
            if ( tamanho >= 4 && tamanho <= 6 ) {DsParm.value = cpf.substr(0,3) + '.' + cpf.substr(3,3);}
            else if ( tamanho >= 7 && tamanho < 10 ) {DsParm.value = cpf.substr(0,3) + '.' + cpf.substr(3,3) + '.' + cpf.substr(6,3);}
            else if ( tamanho >= 10 && tamanho < 14 ) {DsParm.value = cpf.substr(0,3) + '.' + cpf.substr(3,3) + '.' + cpf.substr(6,3) + '-' + cpf.substr(9,2);}
        }   
    }
    else {return false;}    
}  

function CNPJMascarar(DsParm, CdTecla)
{   
    if (CdTecla.keyCode){var tecla = CdTecla.keyCode;}
    else if (CdTecla.which){var tecla = CdTecla.which;}
    else {var tecla = CdTecla.charCode;}
    
    if ( tecla != 9 && tecla != 8 ){if (tecla < 48 || tecla > 57){return false;}}
    var cnpj = '';
    var tamanho = 0;
    cnpj = DsParm.value.replace('.','').replace('.','').replace('-','').replace('/','')
    tamanho = cnpj.length + 1;
    
    if(cnpj.length < 14)
    {
        if ( CdTecla != 9 && CdTecla != 8 ) 
        {
            if ( tamanho > 2 && tamanho < 6 ) {DsParm.value = cnpj.substr(0,2) + '.' + cnpj.substr(2,3);}
            else if ( tamanho >= 6 && tamanho < 9 ) {DsParm.value = cnpj.substr(0,2) + '.' + cnpj.substr(2,3) + '.' + cnpj.substr(5,3);}
            else if ( tamanho >= 9 && tamanho < 13 ) {DsParm.value = cnpj.substr(0,2) + '.' + cnpj.substr(2,3) + '.' + cnpj.substr(5,3) + '/' + cnpj.substr(8,4);}
            else if ( tamanho >= 13 && tamanho < 15 ) {DsParm.value = cnpj.substr(0,2) + '.' + cnpj.substr(2,3) + '.' + cnpj.substr(5,3) + '/' + cnpj.substr(8,4)+ '-' + cnpj.substr(12,2);}
        }
    }
    else {return false;}        
}   

function CEPMascarar(DsParm, CdTecla)
{   
    if (CdTecla.keyCode){var tecla = CdTecla.keyCode;}
    else if (CdTecla.which){var tecla = CdTecla.which;}
    else {var tecla = CdTecla.charCode;}
    
    if ( tecla != 9 && tecla != 8 ){if (tecla < 48 || tecla > 57){return false;}}
    
    var cep = '';
    var tamanho = 0;
    cep = DsParm.value.replace('.','').replace('.','').replace('-','').replace('/','')
    tamanho = cep.length + 1;

    if(cep.length < 8)
    {
        if ( CdTecla != 9 && CdTecla != 8 ) 
        {
            if ( tamanho > 2 && tamanho <= 5 ) {DsParm.value = cep.substr(0,2) + '.' + cep.substr(2,3);}
            else if ( tamanho >= 6 && tamanho <= 8 ) {DsParm.value = cep.substr(0,2) + '.' + cep.substr(2,3) + '-' + cep.substr(5,3);}
        }   
    }
    else {return false;}    
}  

function FoneMascarar(DsParm, CdTecla)
{
    if (CdTecla.keyCode){var tecla = CdTecla.keyCode;}
    else if (CdTecla.which){var tecla = CdTecla.which;}
    else {var tecla = CdTecla.charCode;}
    
    if ( tecla != 9 && tecla != 8 ){if (tecla < 48 || tecla > 57){return false;}}
    
    var DsFone = '';
    DsFone = DsParm.value.replace('.','').replace('.','').replace('-','').replace('/','')

    DsFone = DsFone.toString().replace('-','').replace('-','').replace('-','').replace('-', '' );
    DsFone = DsFone.toString().replace('.','').replace('.','').replace('.','').replace('.', '' ); 
    DsFone = DsFone.toString().replace('/','').replace('/','').replace('/','').replace('/', '' );   
    DsFone = DsFone.toString().replace('(','').replace('(','').replace('(','').replace('(', '' ); 
    DsFone = DsFone.toString().replace(')','').replace(')','').replace(')','').replace(')', '' );
    
    if(DsFone.length <= 10)
    {
    
        Mascarar(DsParm, CdTecla, "(99) 9999-9999");
        return true;
    }
    else if (DsFone.length <= 11)
    {
    
        Mascarar(DsParm, CdTecla, "(99) 99999-9999"); 
        return true;
    }
    else {return false;}
    
    
}
 
function Mascarar(DsParm, CdTecla, DsMascara) 
{           
    if (CdTecla.keyCode){var tecla = CdTecla.keyCode;}
    else if (CdTecla.which){var tecla = CdTecla.which;}
    else {var tecla = CdTecla.charCode;}      

    var campo = DsParm.value;

    campo = campo.toString().replace('-','').replace('-','').replace('-','').replace('-', '' );
    campo = campo.toString().replace('.','').replace('.','').replace('.','').replace('.', '' );
    campo = campo.toString().replace('/','').replace('/','').replace('/','').replace('/', '' );
    campo = campo.toString().replace('(','').replace('(','').replace('(','').replace('(', '' );
    campo = campo.toString().replace(')','').replace(')','').replace(')','').replace(')', '' );
    campo = campo.toString().replace(' ','').replace(' ','').replace(' ','').replace(' ', '' );
    campo = campo.toString().replace(':','').replace(':','').replace(':','').replace(':', '' );
    campo = campo.toString().replace(',','').replace(',','').replace(',','').replace(',', '' );
    campo = campo.toString().replace('$','').replace('$','').replace('$','').replace('$', '' );
    campo = campo.toString().replace('R','').replace('R','').replace('R','').replace('R', '' )
    
    var tamanhocampo = campo.length;
    var tamanhomascara = DsMascara.length;
    var mascaracheck = '-./() :,$R';
    var mascarasaida = '';
    var icampo = 0;
    var imascara = 0;
    
    while (imascara <= tamanhomascara)
    {
        if(mascaracheck.indexOf(DsMascara.charAt(imascara)) == -1)
        {
            mascarasaida += campo.charAt(icampo);
            icampo++;
        }
        else {mascarasaida += DsMascara.charAt(imascara);}
        
        imascara++;
        
        if (icampo > tamanhocampo){break;}
    }
    
    if ((tecla != 8) && (tecla != 9))// backspace
    {
        if(mascarasaida.length == tamanhomascara){return false;}
    
        if (DsMascara.charAt(imascara-1) == "9")  
        {
            if (tecla > 47 && tecla < 58)
            {
                DsParm.value = mascarasaida;
                return true;
            }
            else {return false;}         
        }
        else if(DsMascara.charAt(imascara-1) == "A") 
        {
            if (tecla >= 65 && tecla <= 90)
            {
                DsParm.value = mascarasaida;
                return true;
            } 
            else {return false;}                                                       
        } 
        else {return true;}
    }         
} 

function Mascarar2(DsParm, CdTecla, DsMascara) 
{              
    if (CdTecla.keyCode){var tecla = CdTecla.keyCode;}
    else if (CdTecla.which){var tecla = CdTecla.which;}
    else {var tecla = CdTecla.charCode;}      

    var campo = DsParm.value;

    campo = campo.toString().replace('-','').replace('-','').replace('-','').replace('-', '' );
    campo = campo.toString().replace('.','').replace('.','').replace('.','').replace('.', '' );
    campo = campo.toString().replace('/','').replace('/','').replace('/','').replace('/', '' );
    campo = campo.toString().replace('(','').replace('(','').replace('(','').replace('(', '' );
    campo = campo.toString().replace(')','').replace(')','').replace(')','').replace(')', '' );
    campo = campo.toString().replace(' ','').replace(' ','').replace(' ','').replace(' ', '' );
    campo = campo.toString().replace(':','').replace(':','').replace(':','').replace(':', '' );
    campo = campo.toString().replace(',','').replace(',','').replace(',','').replace(',', '' );
    campo = campo.toString().replace('$','').replace('$','').replace('$','').replace('$', '' );
    campo = campo.toString().replace('R','').replace('R','').replace('R','').replace('R', '' )
    
    var tamanhocampo = campo.length;
    var tamanhomascara = DsMascara.length;
    var mascaracheck = '-./() :,$R';
    var mascarasaida = '';
    var icampo = 0;
    var imascara = 0;
    
    while (imascara <= tamanhomascara)
    {
        if(mascaracheck.indexOf(DsMascara.charAt(imascara)) == -1)
        {
            mascarasaida += campo.charAt(icampo);
            icampo++;
        }
        else {mascarasaida += DsMascara.charAt(imascara);}
        
        imascara++;
        
        if (icampo > tamanhocampo){break;}
    }
    
    if ((tecla != 8) && (tecla != 9))// backspace
    {
        if(mascarasaida.length == tamanhomascara){return false;}
    
        if (DsMascara.charAt(imascara-1) == "9")  
        {
            if (tecla > 47 && tecla < 58)
            {
                DsParm.value = mascarasaida;
                return true;
            }
            else {return false;}         
        }
        else if(DsMascara.charAt(imascara-1) == "A") 
        {
            if (tecla >= 65 && tecla <= 90)
            {
                DsParm.value = mascarasaida;
                return true;
            } 
            else {return false;}                                                       
        } 
        else {return true;}
    }         
} 

function CPFValidar(DsSCR, DsParm)
{
    var DsCPF = DsParm.Value;
    
    DsCPF = DsCPF.toString().replace('-','').replace('-','').replace('-','').replace('-', '' );
    DsCPF = DsCPF.toString().replace('.','').replace('.','').replace('.','').replace('.', '' ); 
    DsCPF = DsCPF.toString().replace('/','').replace('/','').replace('/','').replace('/', '' );    

    if (DsCPF.length != 11) 
    {
        DsParm.IsValid = false;
        return;
    }

    if (!(DsCPF.match(/^[0-9]{3,3}[.]{0,1}[0-9]{3,3}[.]{0,1}[0-9]{3,3}[-]{0,1}[0-9]{2,2}$/)))
    {
        DsParm.IsValid = false;
        return;
    }
            
    var i = 0;
    var soma1 = 0;
    var soma2 = 0;

    for (i = 0; i < 10; i++)
    {
        if (i < 9) {soma1 += (DsCPF.charAt(i)) * (10 - i);}
        soma2 += (DsCPF.charAt(i)) * (11 - i);
    }

    if ((soma1 % 11) < 2) 
    { 
        if (DsCPF.charAt(9) != 0) 
        {
            DsParm.IsValid = false;
            return;
        }
    }
    else 
    { 
        if ((11 - (soma1 % 11)) != (DsCPF.charAt(9)))
        {
            DsParm.IsValid = false;
            return;
        }
    } 
    
    
    if ((soma2 % 11) < 2) 
    { 
        if (DsCPF.charAt(10) != 0)
        {
            DsParm.IsValid = false;
            return;
        }
    }
    else 
    {   
        if ((11 - (soma2 % 11)) != (DsCPF.charAt(10))) 
        {
            DsParm.IsValid = false;
            return;
        } 
    }
    DsParm.IsValid = true;
}

function CNPJValidar(DsSCR, DsParm)
{
    var DsCNPJ = DsParm.Value;
    
    DsCNPJ  = DsCNPJ .toString().replace('-','').replace('-','').replace('-','').replace('-', '' );
    DsCNPJ  = DsCNPJ .toString().replace('.','').replace('.','').replace('.','').replace('.', '' ); 
    DsCNPJ = DsCNPJ.toString().replace('/','').replace('/','').replace('/','').replace('/', '' );    

    if (DsCNPJ.length != 14) 
    {
        DsParm.IsValid = false;
        return;
    }

    if (!(DsCNPJ.match(/^[0-9]{2,2}[.]{0,1}[0-9]{3,3}[.]{0,1}[0-9]{3,3}[/]{0,1}[0-9]{4,4}[-]{0,1}[0-9]{2,2}$/))) 
    {
        DsParm.IsValid = false;
        return;
    }
            
    var i = 0;
    var soma1 = 0;
    var soma2 = 0;

    for (i = 0; i < 13; i++)
    {
        if (i < 4) {soma1 += (DsCNPJ.charAt(i)) * (5 - i);}
        else if (i < 12) {soma1 += (DsCNPJ.charAt(i)) * (13 - i);}

        if (i < 5) {soma2 += (DsCNPJ.charAt(i)) * (6 - i);}
        else if (i < 13) {soma2 += (DsCNPJ.charAt(i)) * (14 - i);} 
    }


    if ((soma1 % 11) < 2) 
    { 
        if (DsCNPJ.charAt(12) != 0) 
        {
            DsParm.IsValid = false;
            return;
        }
    }
    else 
    { 
        if ((11 - (soma1 % 11)) != (DsCNPJ.charAt(12)))
        {
            DsParm.IsValid = false;
            return;
        }
    } 
    
    
    if ((soma2 % 11) < 2) 
    { 
        if (DsCNPJ.charAt(13) != 0)
        {
            DsParm.IsValid = false;
            return;
        }
    }
    else 
    {   
        if ((11 - (soma2 % 11)) != (DsCNPJ.charAt(13))) 
        {
            DsParm.IsValid = false;
            return;
        } 
    }
    DsParm.IsValid = true;
}


function CPFCNPJValidar(DsSCR, DsParm)
{
    var DsCPFCNPJ = DsParm.Value;
    
    DsCPFCNPJ = DsCPFCNPJ .toString().replace('-','').replace('-','').replace('-','').replace('-', '' );
    DsCPFCNPJ = DsCPFCNPJ .toString().replace('.','').replace('.','').replace('.','').replace('.', '' ); 
    DsCPFCNPJ = DsCPFCNPJ.toString().replace('/','').replace('/','').replace('/','').replace('/', '' );    

    if (DsCPFCNPJ.length == 11) 
    { 
        CPFValidar(DsSCR, DsParm);
        return;
    }
    else if (DsCPFCNPJ.length == 14) 
    { 
        CNPJValidar(DsSCR, DsParm);
        return;    
    }
    else
    {
        DsParm.IsValid = false;
        return;
    }    
}

function CEPValidar(DsSCR, DsParm)
{
    var DsCEP = DsParm.Value;
    
    DsCEP = DsCEP.toString().replace('-','').replace('-','').replace('-','').replace('-', '' );
    DsCEP = DsCEP.toString().replace('.','').replace('.','').replace('.','').replace('.', '' ); 
    DsCEP = DsCEP.toString().replace('/','').replace('/','').replace('/','').replace('/', '' );    

    if (DsCEP.length != 8) 
    {
        DsParm.IsValid = false;
        return;
    }

    if (!(DsCEP.match(/^[0-9]{2,2}[.]{0,1}[0-9]{3,3}[-]{0,1}[0-9]{3,3}$/)))
    {
        DsParm.IsValid = false;
        return;
    }
    DsParm.IsValid = true;
}

function FoneValidar(DsSCR, DsParm)
{
    var DsFone = DsParm.Value;
    
    DsFone = DsFone.toString().replace('-','').replace('-','').replace('-','').replace('-', '' );
    DsFone = DsFone.toString().replace('.','').replace('.','').replace('.','').replace('.', '' ); 
    DsFone = DsFone.toString().replace('/','').replace('/','').replace('/','').replace('/', '' );    

    if (DsFone.length < 9) 
    {
        DsParm.IsValid = false;
        return;
    }

    if (!(DsFone.match(/^[(]{0,1}[0-9]{2,2}[)]{0,1}[0-9]{4,4}[.]{0,1}[0-9]{6,6}$/)))
    {
        DsParm.IsValid = false;
        return;
    }
    DsParm.IsValid = true;
}

function MoedaMascarar(DsParm, CdTecla)
{
    //alert(DsParm);
    
    if (CdTecla.keyCode)
    { 
        if (CdTecla.keyCode == 13) {return true;}
        var tecla = CdTecla.keyCode;
    }
    else if (CdTecla.which)
    {   
        if (CdTecla.which == 13) {return true;}
        var tecla = CdTecla.which;
    }
    else 
    {
        if (CdTecla.charCode == 13) {return true;}
        var tecla = CdTecla.charCode;
    }      

    if ( tecla != 9 && tecla != 8 ){if (tecla < 48 || tecla > 57){return false;}}
    
    var campo = DsParm.value;

    var i = j = 0;
    var aux = aux0 = '';
    var strcheck = '1234567890'
    var tamcampo = campo.length;
    var tamaux = tamaux0 = 0;
    
    for(i = 0; i < tamcampo; i++)
    {
        if ((campo.charAt(i) != '0') && (campo.charAt(i) != ',')) {break;}
    }

    for(; i < tamcampo; i++)
    {
        if (strcheck.indexOf(campo.charAt(i))!=-1) aux += campo.charAt(i);
    }
    
    if (tecla >= 48 && tecla <= 57)
    {
        aux += String.fromCharCode(tecla);
    }
     
    tamaux = aux.length;
        
    if (tamaux == 0) {campo = '';}
    else if (tamaux == 1) {campo = '0,0' + aux;}
    else if (tamaux == 2) {campo = '0,' +  aux;}
    else 
    {
        for (j = 0, i = tamaux - 3; i >= 0; i--) 
        {
            if (j == 3)
            {
                aux0 += '.';
                j = 0;
            }
            aux0 += aux.charAt(i);
            j++;
        } 
        campo = '';
        tamaux0 = aux0.length;
        
        for (i = tamaux0 - 1; i >= 0; i--)
        {
            campo += aux0.charAt(i);
        }                    
        campo += "," + aux.substr(tamaux - 2, tamaux);
    }
    DsParm.value = campo;
    return false;
}

function MoedaMascararCasas(DsParm, CdTecla, DsCasas)

{

    if (CdTecla.keyCode)

    { 

        if (CdTecla.keyCode == 13) {return true;}

        var tecla = CdTecla.keyCode;

    }

    else if (CdTecla.which)

    {   

        if (CdTecla.which == 13) {return true;}

        var tecla = CdTecla.which;

    }

    else 

    {

        if (CdTecla.charCode == 13) {return true;}

        var tecla = CdTecla.charCode;

    }      

 

    if ( tecla != 9 && tecla != 8 ){if (tecla < 48 || tecla > 57){return false;}}

    

    var campo = DsParm.value;

 

    var i = j = 0;

    var aux = aux0 = '';

    var strcheck = '1234567890'

    var tamcampo = campo.length;

    var tamaux = tamaux0 = 0;

    

    for(i = 0; i < tamcampo; i++)

    {

        if ((campo.charAt(i) != '0') && (campo.charAt(i) != ',')) {break;}

    }

 

    for(; i < tamcampo; i++)

    {

        if (strcheck.indexOf(campo.charAt(i))!=-1) aux += campo.charAt(i);

    }

    

    if (tecla >= 48 && tecla <= 57)

    {

        aux += String.fromCharCode(tecla);

    }

     

    tamaux = aux.length;

       

    if (tamaux == 0) {campo = '';}

    else if (tamaux <= DsCasas) {campo = '0,' + '0000000000'.substr(1,DsCasas-tamaux) + aux;}

    else 

    {

        for (j = 0, i = tamaux - DsCasas-1; i >= 0; i--) 

        {

            if (j == 3)

            {

                aux0 += '.';

                j = 0;

            }

            aux0 += aux.charAt(i);

            j++;

        } 

        campo = '';

        tamaux0 = aux0.length;

        

        for (i = tamaux0 - 1; i >= 0; i--)

        {

            campo += aux0.charAt(i);

        }                    

        campo += "," + aux.substr(tamaux - DsCasas, tamaux);

    }

    DsParm.value = campo;

    return false;

}


function VerificarBackSpaceCasas(DsParm, CdTecla, DsCasas)

{

    if (CdTecla.keyCode)

    { 

        if (CdTecla.keyCode == 13) {return true;}

        var tecla = CdTecla.keyCode;

    }

    else if (CdTecla.which)

    {   

        if (CdTecla.which == 13) {return true;}

        var tecla = CdTecla.which;

    }

    else 

    {

        if (CdTecla.charCode == 13) {return true;}

        var tecla = CdTecla.charCode;

    }      

 

    if ( tecla == 8 ) {MoedaMascararCasas(DsParm, CdTecla, DsCasas);}

    

}



function VerificarBackSpace(DsParm, CdTecla)
{
    if (CdTecla.keyCode)
    { 
        if (CdTecla.keyCode == 13) {return true;}
        var tecla = CdTecla.keyCode;
    }
    else if (CdTecla.which)
    {   
        if (CdTecla.which == 13) {return true;}
        var tecla = CdTecla.which;
    }
    else 
    {
        if (CdTecla.charCode == 13) {return true;}
        var tecla = CdTecla.charCode;
    }      

    if ( tecla == 8 ) {MoedaMascarar(DsParm, CdTecla);}
    
}


function carregabrw()
{ 
    window.open('', 'SHOPLINE', 'toolbar=yes,menubar=yes,resizable=yes,status=no,scrollbars=yes,width=675,height=485'); 
}


function KeyDownHandler(maskExtenderId) {


    if (navigator.appName != "Microsoft Internet Explorer") {

        

        if (event.keyCode == 35 || event.keyCode == 36) { // Home and End buttons functionality

            

            var txtElement = $get(event.srcElement.id);

            var txtElementText = GetTextElementValue(event.srcElement.id);

            

            if (event.keyCode == 36) {//Home button

                setCaretPosition(txtElement, 0);

            }

            if (event.keyCode == 35) {//End button

                setCaretPosition(txtElement, txtElementText.length);

            }

        }


        if (event.keyCode == 8 || event.keyCode == 46) {

        

            var txtElement = $get(event.srcElement.id);

            var txtElementText = GetTextElementValue(event.srcElement.id);

            var txtElementCursorPosition = doGetCaretPosition(txtElement);

            var maskExtender = $find(maskExtenderId);


            var start = txtElement.selectionStart;

            var end = txtElement.selectionEnd;

            var selectedSymbols = end - start;

            

            if (event.keyCode == 8) //BackSpace

            {

                if (selectedSymbols > 0) {//if there is selection(more then 1 symbol)


                    var str1 = txtElementText.substr(0, start);

                    var str2 = txtElementText.substr(end);

                    var str = str1 + str2;

                    if (str.length < txtElementText.length) str = appendStrWithChar(str, txtElementText, "_");

                    SetTextElementValue(event.srcElement.id, str);

                    //txtElement.value = str;

                    maskExtender._LogicTextMask = deletePromptChars(str, "_");

                    setCaretPosition(txtElement, start);

                }

                else {

                    if ((txtElementCursorPosition - 1) >= 0) {

                        var symbol_to_delete = txtElementText[txtElementCursorPosition - 1];

                        if (symbol_to_delete == "_") {

                            setCaretPosition(txtElement, txtElementCursorPosition - 1);

                        }

                        else {

                            var str1 = txtElementText.substr(0, txtElementCursorPosition - 1);

                            var str2 = txtElementText.substr(txtElementCursorPosition);

                            var str = str1 + str2;

                            if (str.length < txtElementText.length) str = appendStrWithChar(str, txtElementText, "_");

                            SetTextElementValue(event.srcElement.id, str);

                            //txtElement.value = str;

                            maskExtender._LogicTextMask = deletePromptChars(str, "_");

                            setCaretPosition(txtElement, txtElementCursorPosition - 1);

                            //var real_text = deletePromptChars(str, "_");

                        }

                    }

                }



            }

            if (event.keyCode == 46) //Delete

            {

                if (txtElementCursorPosition >= 0 && txtElementCursorPosition < txtElementText.length

                        && ( (selectedSymbols <= 1 && txtElementText[txtElementCursorPosition] != "_") || selectedSymbols > 1) ) { 


                    if (selectedSymbols > 1) {//if there is selection(more then 1 symbol)

                        var str1 = txtElementText.substr(0, start);

                        var str2 = txtElementText.substr(end);

                        var str = str1 + str2;

                        if (str.length < txtElementText.length) str = appendStrWithChar(str, txtElementText, "_");

                        SetTextElementValue(event.srcElement.id, str);

                        //txtElement.value = str;

                        maskExtender._LogicTextMask = deletePromptChars(str, "_");

                        setCaretPosition(txtElement, start);

                    }

                    else {//no selection or 1 symbol selected

                        var symbol_to_delete = txtElementText[txtElementCursorPosition];


                        if (symbol_to_delete != "_") {

                            var str1 = txtElementText.substr(0, txtElementCursorPosition);

                            var str2 = txtElementText.substr(txtElementCursorPosition + 1);

                            var str = str1 + str2;

                            if (str.length < txtElementText.length) str = appendStrWithChar(str, txtElementText, "_");

                            SetTextElementValue(event.srcElement.id, str);

                            //txtElement.value = str;

                            maskExtender._LogicTextMask = deletePromptChars(str, "_");

                            setCaretPosition(txtElement, txtElementCursorPosition);

                        }

                    }

                }



            }

        }


    }


}

function GetTextElementValue(elementId) {

    var textBox = $get(elementId), text;

    if (textBox.AjaxControlToolkitTextBoxWrapper) {

        text = textBox.AjaxControlToolkitTextBoxWrapper.get_Value();

    }

    else {

        text = textBox.value;

    }


    return text;

}


function SetTextElementValue(elementId, someText) {

    var textBox = $get(elementId);

    if (textBox.AjaxControlToolkitTextBoxWrapper) {

        textBox.AjaxControlToolkitTextBoxWrapper.set_Value(someText);

    }

    else {

        textBox.value = someText;

    }

}


function appendStrWithChar(str, templateStr, appChar) {

    var newStr = str;

    var difference = templateStr.length - newStr.length;


    if (difference > 0) {

        for (i = 0; i < difference; i++) { newStr = newStr + "_"; }

    }

    return newStr;

}


function deletePromptChars(str, promptChar) {

    var newStr = str;

    for (i = 0; i < newStr.length; i++) {

        if (str[i] == promptChar) {

            newStr = newStr.substr(0, i);

            return newStr;

        }

    }

}


function doGetCaretPosition(ctrl) {

    var CaretPos = 0; // IE Support

    if (document.selection) {

        ctrl.focus();

        var Sel = document.selection.createRange();

        Sel.moveStart('character', -ctrl.value.length);

        CaretPos = Sel.text.length;

    }

    // Firefox support

    else if (ctrl.selectionStart || ctrl.selectionStart == '0')

        CaretPos = ctrl.selectionStart;

    return (CaretPos);

}


function setCaretPosition(ctrl, pos) {

    if (ctrl.setSelectionRange) {

        ctrl.focus();

        ctrl.setSelectionRange(pos, pos);

    }

    else if (ctrl.createTextRange) {

        var range = ctrl.createTextRange();

        range.collapse(true);

        range.moveEnd('character', pos);

        range.moveStart('character', pos);

        range.select();

    }

}


function ScrollableTable (SpanId, tableHeight, tableWidth, freezeRow, freezeCol) {

   var theSpan  = document.getElementById(SpanId);
   var tbl  = theSpan.getElementsByTagName('table');
   var tableEl = tbl[0];

 initIEengine = function () {

  this.containerEl.style.overflowY = 'auto';
  this.containerEl.style.overflowX = 'auto';

  // Trava linhas e colunas 
  var trs = tableEl.getElementsByTagName('tr');
  for (x=0; x<trs.length; x++) {
   var tds = trs[x].getElementsByTagName('td');
   for (y=0; y<tds.length; y++) {
    if(x <= freezeRow && y <= freezeCol) {
     tds[y].style.position ='relative';
     tds[y].style.setExpression("top",  "this.parentElement.parentElement.parentElement.parentElement.scrollTop + 'px'");
     tds[y].style.setExpression("left",  "this.parentElement.parentElement.parentElement.parentElement.scrollLeft + 'px'");
     tds[y].style.setExpression("zIndex",  "'2'");
    }
    else if(x > freezeRow && y <= freezeCol) {
     tds[y].style.position ='relative';
     tds[y].style.setExpression("left",  "this.parentElement.parentElement.parentElement.parentElement.scrollLeft + 'px'");
     tds[y].style.setExpression("zIndex",  "'1'");
    }
    else if(x <= freezeRow && y > freezeCol) {
     tds[y].style.position ='relative';
     tds[y].style.setExpression("top",  "this.parentElement.parentElement.parentElement.parentElement.scrollTop + 'px'");
     tds[y].style.setExpression("zIndex",  "'1'");
    }
   }
  }

  eval("window.attachEvent('onresize', function () { document.getElementById('" + tableEl.id + "').style.visibility = 'hidden'; document.getElementById('" + tableEl.id + "').style.visibility = 'visible'; } )");
 };
  
 this.scrollWidth = 16;

 this.originalHeight = tableEl.clientHeight;
 this.originalWidth = tableEl.clientWidth;

 this.newHeight = parseInt(tableHeight);
 this.newWidth = tableWidth ? parseInt(tableWidth) : this.originalWidth;

 tableEl.style.height = 'auto';
 tableEl.removeAttribute('height');

 this.containerEl = tableEl.parentNode.insertBefore(document.createElement('div'), tableEl);
 this.containerEl.appendChild(tableEl);
 this.containerEl.style.height = tableHeight;
 this.containerEl.style.width = tableWidth;

 var thead = tableEl.getElementsByTagName('thead');
 thead = (thead[0]) ? thead[0] : null;

 var tfoot = tableEl.getElementsByTagName('tfoot');
 tfoot = (tfoot[0]) ? tfoot[0] : null;

 var tbody = tableEl.getElementsByTagName('tbody');
 tbody = (tbody[0]) ? tbody[0] : null;

 if (document.all && document.getElementById && !window.opera) initIEengine();
}

function AtivaDivProgress() {
    document.getElementById("divProgress").style.display = "block";
}



function contarCaracteres2(obj, divID, maxchar) {

    objDiv = get_object(divID);

    if (this.id) obj = this;

    var remaningChar = maxchar - (obj.value.length);

    if (objDiv) {
        objDiv.innerHTML = "(faltam " + remaningChar + " de " + maxchar + " caracteres)";
    }

    if (remaningChar <= 0) {
        obj.value = obj.value.substring(maxchar, 0);

        if (objDiv) {
            objDiv.innerHTML = "(faltam 0 de " + maxchar + " caracteres)";
        }

        return false;
    }
    else { return true; }
}

function contarCaracteres(obj, divID, maxchar) {

    objDiv = get_object(divID);

    if (this.id) obj = this;

    var remaningChar = maxchar - (obj.value.length);

    if (objDiv) {
        objDiv.innerHTML = "( " + obj.value.length + " de " + maxchar + " )";
        /*"(faltam " + remaningChar + " de " + maxchar + " caracteres)";*/
    }

    if (remaningChar <= 0) {
        obj.value = obj.value.substring(maxchar, 0);

        if (objDiv) {
            objDiv.innerHTML = "( " + obj.value.length + " de " + maxchar + " )";
        }

        return false;
    }
    else { return true; }
}

function get_object(id) {

    var object = null;

    if (document.layers) {
        object = document.layers[id];
    }
    else if (document.all) {
        object = document.all[id];
    }
    else if (document.getElementById) {
        object = document.getElementById(id);
    }

    return object;
}