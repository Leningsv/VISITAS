// JScript File
function window_onload()     
 {
      
     var Pagina;
     var directorio;
      //Llamo a Script de Control de Grabación de Imagenes
     document.onload=trap;
     //Llamo a Script de Control de Teclas
     // document.onkeypress= mykeyhandler;
     //Obtengo la Pagina
       Pagina= Pagina_nombre(1); 
      //Obtengo el directorio
       directorio= Pagina_nombre(0); 
       //valido Bloqueos
        valida_bloqueos(Pagina,directorio); 
      //valido Pagina con frame
      valida_frame(Pagina);          
     //valido status bar
       texto_inicio();

   
       
  } 
            
function ratonAbajo(LePucho)
    {  
        if(document.layers&&LePucho.which!=1) 
            return false;
        if(document.all||document.getElementById)
            return false;
    } 		
function mykeyhandler() 

    {
//       if(	window.event.keyCode==17 ) //|| window.event.keyCode==18)
//        {
//            alert('No puede usar esa Función en la Web del Teatro');
//        }
    	
////        if(	(window.event && window.event.keyCode == 8) || 
////            (window.event && window.event.keyCode == 122)) 
////            {
////                // try to cancel the backspace
////                window.event.cancelBubble = true;
////                window.event.keyCode = 8;
////                window.event.returnValue = false;
////                return false;
////            }
    }

function protect(e) 
    {
        alert("Lo sentimos, no puedes guardar la imagen");
        return false;
    }

function trap() 
    {

        if(document.images)

        for(i=0;i<document.images.length;i++)

        document.images[i].onmousedown = protect;

    }
 
function disableselect(e)
    {
        return false
    }
function reEnable()
    {
        return true
    }

function Pagina_nombre(tipo)
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
 function bloquea_atras()
    {
     //evita el boton atras
     history.forward(1);
    
    }
 function deselecciona()
 {
 
 //Script para deseleccionar con el mouse      
    document.onselectstart=new Function ("return false")
    if (window.sidebar)
    {
        document.onmousedown=disableselect;
        document.onclick=reEnable;   
    } 
 } 
 function valida_frame(Pagina)
 {
    //Script para llamar a Pagina con frames
             if (parent.location.href == self.location.href)
              {          
                    if (Pagina != 'Frm_Login.aspx')
                    {
                      if (Pagina!='ActivMasterDet.aspx' && Pagina!='MantActivoReval.aspx')
                        {
                          if (Pagina != 'Default.aspx'&& Pagina != 'Index.aspx')
                          {
                              window.location.href = 'Default.aspx';
                              window.menubar=0;
                          }
                          else
                          {
                             window.parent.location = 'Default.aspx';
                              window.menubar=0;
                          }
                         }
                    }
              }
 }  
 
 function valida_bloqueos(Pagina,directorio)
 {  
               valida_navegador(Pagina); 
               //deselecciona();
                bloquea_atras();
                valida_resolucion();
                  
 }
 function valida_resolucion()
 {
         // Recomendar y detectar resolucion de pantalla
            var ancho = 1024;
            var alto = 768;      
            // Mensaje que podemos modificar
//            if (screen.width < ancho || screen.height < alto) 
//            {
//                msg = "Estimado Usuario esta Pagina se visualiza mejor con resolucion "
//                + "de pantalla " + ancho + "x" + alto + " "
//                + "pero tu resolucion es " + screen.width + "x"
//                + screen.height + "./nPor favor cambiala "
//                + "para poder visualizar mejor esta Pagina. Gracias!! ";     
//                alert(msg);
//             }   
 }
 
 function valida_navegador(Pagina)
 {
  
//  alert(navigator.appVersion);
  var isIE4 ;
  isIE4 = (navigator.appVersion.charAt(0)>=4 && (navigator.appVersion).indexOf("MSIE") != -1);
  if (navigator.appName != 'Microsoft Internet Explorer') 
    {

                if (Pagina != 'Frm_Login.aspx')
                    {
                 alert("Estimado Usuario este sitio se visualiza mejor con Microsoft Internet Explorer\n Si dispone de este navegador es preferible que lo use");
                 window.parent.location="Default.aspx";
                 //alert(Pagina);
                 window.menubar=0;
                 }
    }

 }
 
 
 
 
 function popUp(URL) 
  {
    day = new Date();
    id = day.getTime();
    eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=1,scrollbars=1,location=1,statusbar=0,men ubar=0,resizable=1,width=800,height=450');");
  }
  
   
if(document.layers)
    { 
        window.captureEvents(Event.MOUSEDOWN);
        window.onMouseDown=ratonAbajo;
    }
    else  
        document.oncontextmenu=ratonAbajo;
        
//Animación de Status Bar

var strtexto = "PeopleSINet"
var strmitexto = "";
var started = false;
var intpaso = 0;
var times = 1;
function texto_inicio() {
times--;
if (!times) {
if (!started) {
started = true;
window.status = strtexto;
setTimeout("animar_textos()", 1);
}
strmitexto = strtexto;
}
}
function animar_textos() {
intpaso++;
if (intpaso==7) intpaso = 1;
if (intpaso==1) window.status = '>===' + strmitexto + '===<';
if (intpaso==2) window.status = '=>==' + strmitexto + '==<=';
if (intpaso==3) window.status = '>=>=' + strmitexto + '=<=<';
if (intpaso==4) window.status = '=>=>' + strmitexto + '<=<=';
if (intpaso==5) window.status = '==>=' + strmitexto + '=<==';
if (intpaso==6) window.status = '===>' + strmitexto + '<===';
setTimeout("animar_textos()", 200);

}


var _jslib_isIE=document.all?true:false;
var _jslib_isNS=document.layers?true:false;
var _jslib_isNS6=document.getElementById&&!document.all?true:false;

function jsGetObject(name) {
    if(_jslib_isIE) {
        return document.all[name];
    } else if(_jslib_isNS) {
        return document.layers[name];
    } else if (_jslib_isNS6) {
        return document.getElementById(name);
    }
    
    return null;
}

