// JScript File
function valida_numero(strcampo,strseparador)
{
    strseparador='.';
    if (event.keyCode < 48 || event.keyCode > 57) 
     {
          if (strseparador == 'int')
           {
               event.returnValue = false;
           }
           else
           {
              if (event.keyCode != strseparador.charCodeAt(0))
                  {
                    event.returnValue = false;
                  }
              else
                 {			  
				     if (document.getElementById(strcampo).value.length < 1 )
                       {
                         event.returnValue = false; 
                       }
                       else
                       {
					     if (document.getElementById(strcampo).value.indexOf(strseparador) != -1)
                         {
                           event.returnValue = false; 
                         } 

                       }
                 }    
              }
     }
 }
 /*************************DESDE AQUI LC*************************/ 
var objeto2;   
function soloDinero(objeto, e){
    var keynum
    var keychar
    var numcheck  
    objeto.value = objeto.value.replace(',', '.');
    if(window.event){ /*/ IE*/
        keynum = e.keyCode
    }else if(e.which){ /*/ Netscape/Firefox/Opera/*/
             keynum = e.which
    }
    if((keynum>=35 && keynum<=37) ||keynum==8||keynum==9||keynum==46||keynum==39) {
        return true;
    }
    if(keynum==190||keynum==110||(keynum>=95&&keynum<=105)||(keynum>=48&&keynum<=57)){
        posicion = objeto.value.indexOf('.');
        if(posicion==-1) {
            return true;
        }else {                           
            if(!(keynum==190||keynum==110)){
                objeto2=objeto;
                t = setTimeout('dosDecimales()',150);
                return true;
            }else{
                objeto2=null;
                return false;
            }
        }

    }else {
        return false;
    }
}

function dosDecimales(){    
    var objeto = objeto2;
    var posicion = objeto.value.indexOf('.');
    var decimal = 2;
    if(objeto.value.length - posicion < decimal){
        objeto.value = objeto.value.substr(0,objeto.value.length-1);
    }else {
        objeto.value = objeto.value.substr(0,posicion+decimal+1);
    }
    return;
}
  
/**************************HASTA AQUI*********************************/

function asigna_valor(strcampo,strcamposer, bantipo)
{
  if (bantipo==0)
  {
     document.getElementById(strcampo).value = document.getElementById(strcamposer).value;
  }
  else
   {
    document.getElementById(strcamposer).value = document.getElementById(strcampo).value;
   } 
}
function asigna_valor_externo(strcampo,strcamposer, valor)
{

     window.opener.document.getElementById(strcampo).value=valor;
     window.opener.document.getElementById(strcamposer).value=valor;
}

strnewcadena='';
function valida_valor(strcontrole,strcontrolgen,strcontenedor)
  {
      intval = Number(document.getElementById(strcontrolgen).value);
      intvaloritem = document.getElementById(strcontenedor + strcontrole).value;
      remplazar(intvaloritem,",",".");
      intvaloritem = strnewcadena;
      intauxiliar=Number(intvaloritem);
     if ( Number(intvaloritem)<= 100 )
     {
        
         intcont=1;
         inttotal=0;
         intvaloritem=0;
         while (intcont <= intval)
         {
             strcontrol=strcontenedor + "txtporcen" + intcont;
             intvaloritem = document.getElementById(strcontrol).value;
             remplazar(intvaloritem,",",".");
			 intvaloritem=Number(strnewcadena);	
             inttotal=Number(intvaloritem) + Number(inttotal);
			 //alert(intcont + ' = ' + intvaloritem);
			 //alert('Total = ' + inttotal);
             intcont=intcont + 1 ;
         }
         if (inttotal > 100)
            
           {
             intcont=intval;
             while (inttotal > 100)
              {
                 
				 strcontrol=strcontenedor + "txtporcen" + intcont;
                 
                 intval1=document.getElementById(strcontrol).value;
                 remplazar(intval1,",",".");
                 intval1=Number(strnewcadena);

                 if ((inttotal-intval1) < 100)
                  {
					 intvalorreal=inttotal-intval1
					 dblnewvalordep= 100-intvalorreal;
					 dblnewvalordep=Math.round(dblnewvalordep*100)/100;
					 document.getElementById(strcontrol).value=dblnewvalordep;
                     inttotal=inttotal-intval1
                     intcont=intcont-1
                  }
                 else
                  {
					 document.getElementById(strcontrol).value=0;
                     inttotal=inttotal-intval1
                     intcont=intcont-1
                  } 
              }
           }
         else if (inttotal < 100)
		   {
		     intcont=intval;
                
                 strcontrol=strcontenedor + "txtporcen" + intcont;
				 intval1=document.getElementById(strcontrol).value;
                 remplazar(intval1,",",".");
                 intval1=Number(strnewcadena);
				 intvalorreal=inttotal-intval1;
                 dblnewvalordep= 100-intvalorreal;
				 dblnewvalordep=Math.round(dblnewvalordep*100)/100;
			     document.getElementById(strcontrol).value=dblnewvalordep;
				 
		   }
             intcont=1;
			  strcontrol=strcontenedor + "txtporcen" + intcont;
			  //alert(strcontrol);
			  //alert(document.getElementById(strcontrol).value);
             intvaloritem = document.getElementById(strcontrol).value;
			  if (intvaloritem = '')
			    {
				   //alert('vacio');
				}
			else
               {	
				 //alert(intvaloritem);
				 document.getElementById("ctl00$ContentPlaceHolder1$tabCategoria$Tabtabla$hiddatostabdepre").value ='';
				 while (intcont <= intval)
			         {
					     strcontrol=strcontenedor + "txtporcen" + intcont;
			             intvaloritem = document.getElementById(strcontrol).value;
			             remplazar(intvaloritem,",",".");
			             intvaloritem=Number(strnewcadena); 
						 if (intcont > 1)
						  {
						    document.getElementById("ctl00$ContentPlaceHolder1$tabCategoria$Tabtabla$hiddatostabdepre").value = document.getElementById("ctl00$ContentPlaceHolder1$tabCategoria$Tabtabla$hiddatostabdepre").value + ';' + intvaloritem;
						  }
						  else
						  {
						    document.getElementById("ctl00$ContentPlaceHolder1$tabCategoria$Tabtabla$hiddatostabdepre").value =  intvaloritem;
						  }
			             inttotal=Number(intvaloritem) + inttotal;
			             intcont=intcont+1;
					 }
					 //alert(document.getElementById("ctl00$ContentPlaceHolder1$tabCategoria$Tabtabla$hiddatostabdepre").value); 
				}
	 }
     else
     {
        alert('No puede ingresar valores mayores al 100%');
        document.getElementById(strcontenedor + strcontrole).value = 100;
        intcont=1;
         inttotal=0
         while (intcont <= intval)
         {
             strcontrol=strcontenedor + "txtporcen" + intcont;
             intvaloritem = document.getElementById(strcontrol).value;
             remplazar(intvaloritem,",",".");
             intvaloritem=Number(strnewcadena); 
             inttotal=Number(intvaloritem) + inttotal;
             intcont=intcont+1;
         }
        intcont=intval;
             while (inttotal > 100)
              {
                 strcontrol=strcontenedor + "txtporcen" + intcont;
                 intval1=document.getElementById(strcontrol).value;
                 remplazar(intval1,",",".");
                 intval1=Number(intval1);

                 if ((inttotal-intval1) < 100)
                  {
                     intvalorreal=inttotal-intval1
					 dblnewvalordep= 100-intvalorreal;
					 dblnewvalordep=Math.round(dblnewvalordep*100)/100;
					 document.getElementById(strcontrol).value=dblnewvalordep;
                     inttotal=inttotal-intval1
                     intcont=intcont-1
                  }
                 else
                  {
                     document.getElementById(strcontrol).value=0;
                     inttotal=inttotal-intval1
                     intcont=intcont-1
                  } 
              }
			  intcont=1;
			  strcontrol=strcontenedor + "txtporcen" + intcont;
			  //alert(strcontrol);
			 // alert(document.getElementById(strcontrol).value);
             intvaloritem = document.getElementById(strcontrol).value;
			  if (intvaloritem = '')
			    {
				   //alert('vacio');
				}
			else
               {			
				  //alert(intvaloritem);
				  document.getElementById("ctl00$ContentPlaceHolder1$tabCategoria$Tabtabla$hiddatostabdepre").value ='';
			     while (intcont <= intval)
			         {
					     strcontrol=strcontenedor + "txtporcen" + intcont;
			             intvaloritem = document.getElementById(strcontrol).value;
			             remplazar(intvaloritem,",",".");
			             intvaloritem=Number(strnewcadena); 
						 if (intcont > 1)
						  {
						    document.getElementById("ctl00$ContentPlaceHolder1$tabCategoria$Tabtabla$hiddatostabdepre").value = document.getElementById("ctl00$ContentPlaceHolder1$tabCategoria$Tabtabla$hiddatostabdepre").value + ';' + intvaloritem;
						  }
						  else
						  {
						    document.getElementById("ctl00$ContentPlaceHolder1$tabCategoria$Tabtabla$hiddatostabdepre").value =  intvaloritem;
						  }
			             inttotal=Number(intvaloritem) + inttotal;
			             intcont=intcont+1;
					 }
					//alert(document.getElementById("ctl00$ContentPlaceHolder1$tabCategoria$Tabtabla$hiddatostabdepre").value); 
		        }
	 }
  }
  
  function valida_valor1(strcontrole,strcontrolgen,strcontenedor)
  {
      intval = Number(document.getElementById(strcontrolgen).value);
      intvaloritem = document.getElementById(strcontenedor + strcontrole).value;
      remplazar(intvaloritem,",",".");
      intvaloritem = strnewcadena;
      intauxiliar=Number(intvaloritem);
     if ( Number(intvaloritem)<= 100 )
     {
        
         intcont=1;
         inttotal=0;
         intvaloritem=0;
         while (intcont <= intval)
         {
             strcontrol=strcontenedor + "txtporcen" + intcont;
             intvaloritem = document.getElementById(strcontrol).value;
             remplazar(intvaloritem,",",".");
			 intvaloritem=Number(strnewcadena);	
             inttotal=Number(intvaloritem) + Number(inttotal);
			 //alert(intcont + ' = ' + intvaloritem);
			 //alert('Total = ' + inttotal);
             intcont=intcont + 1 ;
         }
         if (inttotal > 100)
            
           {
             intcont=intval;
             while (inttotal > 100)
              {
                 
				 strcontrol=strcontenedor + "txtporcen" + intcont;
                 
                 intval1=document.getElementById(strcontrol).value;
                 remplazar(intval1,",",".");
                 intval1=Number(strnewcadena);

                 if ((inttotal-intval1) < 100)
                  {
					 intvalorreal=inttotal-intval1
					 dblnewvalordep= 100-intvalorreal;
					 dblnewvalordep=Math.round(dblnewvalordep*100)/100;
					 document.getElementById(strcontrol).value=dblnewvalordep;
                     inttotal=inttotal-intval1
                     intcont=intcont-1
                  }
                 else
                  {
					 document.getElementById(strcontrol).value=0;
                     inttotal=inttotal-intval1
                     intcont=intcont-1
                  } 
              }
           }
         else if (inttotal < 100)
		   {
		     intcont=intval;
                
                 strcontrol=strcontenedor + "txtporcen" + intcont;
				 intval1=document.getElementById(strcontrol).value;
                 remplazar(intval1,",",".");
                 intval1=Number(strnewcadena);
				 intvalorreal=inttotal-intval1;
                 dblnewvalordep= 100-intvalorreal;
				 dblnewvalordep=Math.round(dblnewvalordep*100)/100;
			     document.getElementById(strcontrol).value=dblnewvalordep;
				 
		   }
             intcont=1;
			  strcontrol=strcontenedor + "txtporcen" + intcont;
			  //alert(strcontrol);
			  //alert(document.getElementById(strcontrol).value);
             intvaloritem = document.getElementById(strcontrol).value;
			  if (intvaloritem = '')
			    {
				   //alert('vacio');
				}
			else
               {	
				 //alert(intvaloritem);
				 document.getElementById("ctl00$ContentPlaceHolder1$tabactivo$Tabtabla$hiddatostabdepre").value ='';
				 while (intcont <= intval)
			         {
					     strcontrol=strcontenedor + "txtporcen" + intcont;
			             intvaloritem = document.getElementById(strcontrol).value;
			             remplazar(intvaloritem,",",".");
			             intvaloritem=Number(strnewcadena); 
						 if (intcont > 1)
						  {
						    document.getElementById("ctl00$ContentPlaceHolder1$tabactivo$Tabtabla$hiddatostabdepre").value = document.getElementById("ctl00$ContentPlaceHolder1$tabactivo$Tabtabla$hiddatostabdepre").value + ';' + intvaloritem;
						  }
						  else
						  {
						    document.getElementById("ctl00$ContentPlaceHolder1$tabactivo$Tabtabla$hiddatostabdepre").value =  intvaloritem;
						  }
			             inttotal=Number(intvaloritem) + inttotal;
			             intcont=intcont+1;
					 }
					 //alert(document.getElementById("ctl00$ContentPlaceHolder1$tabactivo$Tabtabla$hiddatostabdepre").value); 
				}
	 }
     else
     {
        alert('No puede ingresar valores mayores al 100%');
        document.getElementById(strcontenedor + strcontrole).value = 100;
        intcont=1;
         inttotal=0
         while (intcont <= intval)
         {
             strcontrol=strcontenedor + "txtporcen" + intcont;
             intvaloritem = document.getElementById(strcontrol).value;
             remplazar(intvaloritem,",",".");
             intvaloritem=Number(strnewcadena); 
             inttotal=Number(intvaloritem) + inttotal;
             intcont=intcont+1;
         }
        intcont=intval;
             while (inttotal > 100)
              {
                 strcontrol=strcontenedor + "txtporcen" + intcont;
                 intval1=document.getElementById(strcontrol).value;
                 remplazar(intval1,",",".");
                 intval1=Number(intval1);

                 if ((inttotal-intval1) < 100)
                  {
                     intvalorreal=inttotal-intval1
					 dblnewvalordep= 100-intvalorreal;
					 dblnewvalordep=Math.round(dblnewvalordep*100)/100;
					 document.getElementById(strcontrol).value=dblnewvalordep;
                     inttotal=inttotal-intval1
                     intcont=intcont-1
                  }
                 else
                  {
                     document.getElementById(strcontrol).value=0;
                     inttotal=inttotal-intval1
                     intcont=intcont-1
                  } 
              }
			  intcont=1;
			  strcontrol=strcontenedor + "txtporcen" + intcont;
			  //alert(strcontrol);
			 // alert(document.getElementById(strcontrol).value);
             intvaloritem = document.getElementById(strcontrol).value;
			  if (intvaloritem = '')
			    {
				   //alert('vacio');
				}
			else
               {			
				  //alert(intvaloritem);
				  document.getElementById("ctl00$ContentPlaceHolder1$tabactivo$Tabtabla$hiddatostabdepre").value ='';
			     while (intcont <= intval)
			         {
					     strcontrol=strcontenedor + "txtporcen" + intcont;
			             intvaloritem = document.getElementById(strcontrol).value;
			             remplazar(intvaloritem,",",".");
			             intvaloritem=Number(strnewcadena); 
						 if (intcont > 1)
						  {
						    document.getElementById("ctl00$ContentPlaceHolder1$tabactivo$Tabtabla$hiddatostabdepre").value = document.getElementById("ctl00$ContentPlaceHolder1$tabactivo$Tabtabla$hiddatostabdepre").value + ';' + intvaloritem;
						  }
						  else
						  {
						    document.getElementById("ctl00$ContentPlaceHolder1$tabactivo$Tabtabla$hiddatostabdepre").value =  intvaloritem;
						  }
			             inttotal=Number(intvaloritem) + inttotal;
			             intcont=intcont+1;
					 }
					//alert(document.getElementById("ctl00$ContentPlaceHolder1$tabactivo$Tabtabla$hiddatostabdepre").value); 
		        }
	 }
  }
function remplazar(strcadena,strcar1,strcar2)
{
intcont2=0;
strnewcadena='';
while (intcont2< strcadena.length)
 {
    if (strcadena.substring(intcont2,1+intcont2)== strcar1)
      {
         
           strnewcadena= strnewcadena + strcar2;
         
      }
    else
      {
       strnewcadena= strnewcadena + strcadena.substring(intcont2,1+intcont2)
      } 
   
   intcont2=intcont2+1;
 } 

 

}