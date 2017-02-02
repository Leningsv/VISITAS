// JScript File
function window_onload()     
 {
     var pagina;
      pagina= pagina_nombre(1); 
       valida_navegador(pagina)
  } 

function valida_navegador(pagina)
 {
 if(navigator.appName != 'Microsoft Internet Explorer' ) 
    {
               
                if (pagina != 'Frm_Login.aspx')
                    {
                     alert("Estimado Usuario este sitio se visualiza mejor con Microsoft Internet Explorer\n Si dispone de este navegador es preferible que lo use");
                     window.parent.location = "Frm_Login.aspx";
                     //alert(pagina);
                     window.menubar=1;
                     window.toolbar.visibility=YES
                     
                     }
    }
    else
    {
       if (pagina == 'Default.aspx' )
          {
                 window.parent.location="Frm_Login.aspx";
             
          }
      else if (pagina == '')
          {
       window.parent.location="Frm_Login.aspx";
             
          }
                  
   }

 }
 
function pagina_nombre(tipo)
    {
         var pagename;
         var intcont;
         var nombre;
         var bolubica;
         var intdirectorio;
         var intfinpage;
           pagename=self.location.href;
           intcont=pagename.length -1; 
           nombre='';
           bolubica=0;
           intfinpage=pagename.length;
           while (intcont > 0)
           {
             if (pagename.substring(intcont,intcont+1)=="?")
             {
               intfinpage=intcont;
             }
             else if (pagename.substring(intcont,intcont+1)=="/")
                 {               
                   if (tipo == 1)
                   {
                       nombre=pagename.substring(intcont+1,intfinpage);
                       intcont = 0
                   }
                   else
                   {
                      if (bolubica == 0)
                      {
                        bolubica=1;
                        intdirectorio=intcont;
                      }
                      else
                      {
                         nombre=pagename.substring(intcont+1,intdirectorio);
                         intcont = 0
                      }
                   }   
                 }
             intcont=intcont-1;
           }
           return nombre;
    } 