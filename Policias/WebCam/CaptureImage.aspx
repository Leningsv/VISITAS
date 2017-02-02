<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaptureImage.aspx.cs" Inherits="Policias_WebCam_CaptureImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <script language="javascript" type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(["_setAccount", "UA-1101037-2"]);
        _gaq.push(["_trackPageview"]);

        (function () {
            var ga = document.createElement("script"); ga.type = "text/javascript"; ga.async = true;
            ga.src = ("https:" == document.location.protocol ? "https://ssl" : "http://www") + ".google-analytics.com/ga.js";
            var s = document.getElementsByTagName("script")[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>
    <link rel="stylesheet" type="text/css" href="css/Master.css" />
    <style type="text/css">
        #webcam, #canvas
        {
            width: 272px;
            border: 1px solid #ccc;
            background: #eee;
        }

        #webcam
        {
            position: relative;
            margin-top: 5px;
            margin-bottom: 10px;
        }

            #webcam > span
            {
                z-index: 2;
                position: absolute;
                color: #eee;
                font-size: 10px;
                bottom: -16px;
                left: 152px;
            }

            #webcam > img
            {
                z-index: 1;
                position: absolute;
                border: 0px none;
                padding: 0px;
                bottom: -40px;
                left: 89px;
            }

            #webcam > div
            {
                border: 1px solid #ccc;
                position: absolute;
                right: -90px;
                padding: 5px;
                cursor: pointer;
            }

            #webcam a
            {
                background: #fff;
                font-weight: bold;
            }

                #webcam a > img
                {
                    border: 0px none;
                }

        #canvas
        {
            border: 1px solid #ccc;
            background: #eee;
        }

        #flash
        {
            position: absolute;
            top: 0px;
            left: 0px;
            z-index: 5000;
            width: 100%;
            height: 500px;
            background-color: #c00;
            display: none;
        }

        object
        {
            display: block; /* HTML5 fix */
            position: relative;
            z-index: 1000;
        }
    </style>
    <link href="css/Master.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="Scripts/jquery.webcam.min.js" type="text/javascript"></script>
</head>
<body>
    <div class="PhotoUploadWrapper">
        <!--div class="PhotoUpoloadCoseBtn">
        </div>-->
        <div class="PhotoUploadContent">
            <div class="PhotoUpoloadHeader">
                &nbsp;</div>
            <div class="PhotoUpoloadLeft">
                <div class="PhotoUpoloadRightHeader">
                    <p style="float: left; font-family: Verdana, Geneva, sans-serif; font-size: 14px; line-height: 35px; text-indent: 18px; font-weight: bold; color: #FFF; width: 308px;">
                        Mostrar Camara Web</p>

                </div>
                <div class="PhotoUpoloadLeftMainCont">
                    <div class="photo_selected_BG">
                        <div style="padding: 20px 0px 0px 24px;">
                            <div id="webcam">
                            </div>
                        </div>
                    </div>
                    <div style="text-align: center; margin-bottom: 46px;">


                        <%--<a href="javascript:webcam.capture();changeFilter();void(0);">Take a picture instantly</a>--%> 

                        <a href="#" id ="fil" onclick = "javascript:webcam.capture();changeFilter();void(0);">

<%--                            <input type="image" id="capture" onclick="javascript: document.getElementById('Submit').disabled = false;"
                                src="images/boton1.png" alt="#" /></a>  --%>
                                
                                <input type="image" id="capture" runat="server" src="images/boton1.png" /></a>                                                       
                                
                    </div>
                </div>
            </div>
            <div class="PhotoUpoloadRight">
                <div class="PhotoUpoloadLeftHeader">
                    <p style="float: left; font-family: Verdana, Geneva, sans-serif; font-size: 14px; line-height: 35px; text-indent: 18px; font-weight: bold; color: #FFF; width: 319px;">
                        Imagen Obtenida
                    </p>

                </div>
                <div class="photo_selected_BG">
                    <div style="padding: 26px 0px 0px 25px;">
                        <canvas id="canvas" width="320" height="240"></canvas>
                    </div>
                </div>
                <div style="text-align: center; margin-bottom: 46px;">
                    <a href="#" id="filter" onclick="javascript:UploadPic();">

                        <input type="image" id="Submit" runat="server" src="images/boton2.png" /></a>                    
                </div>
            </div>
        </div>
    </div>


    <script language="javascript" type="text/javascript">


        var url = location.href;
        url = url.replace(/.*\?(.*?)/, "$1");
        Variables = url.split("&");
        for (i = 0; i < Variables.length; i++) {
            Separ = Variables[i].split("=");
            eval('var ' + Separ[0] + '="' + Separ[1] + '"');
        }

        var cod_visi = codigovisitante

        var pos = 0;
        var ctx = null;
        var cam = null;
        var image = null;

        var filter_on = false;
        var filter_id = 0;

        function changeFilter() {
            if (filter_on) {
                filter_id = (filter_id + 1) & 7;
            }
        }

        function toggleFilter(obj) {
            if (filter_on = !filter_on) {
                obj.parentNode.style.borderColor = "#c00";
            } else {
                obj.parentNode.style.borderColor = "#333";
            }
        }

        jQuery("#webcam").webcam({

            //width: 272,
            width: 272,
            height: 202,
            mode: "callback",
            swffile: "jscam_canvas_only.swf",

            onTick: function (remain) {

                if (0 == remain) {
                    jQuery("#status").text("Cheese!");
                } else {
                    jQuery("#status").text(remain + " seconds remaining...");
                }
            },

            onSave: function (data) {

                var col = data.split(";");
                var img = image;

                if (false == filter_on) {

                    for (var i = 0; i < 320; i++) {
                        var tmp = parseInt(col[i]);
                        img.data[pos + 0] = (tmp >> 16) & 0xff;
                        img.data[pos + 1] = (tmp >> 8) & 0xff;
                        img.data[pos + 2] = tmp & 0xff;
                        img.data[pos + 3] = 0xff;
                        pos += 4;
                    }

                } else {

                    var id = filter_id;
                    var r, g, b;
                    var r1 = Math.floor(Math.random() * 255);
                    var r2 = Math.floor(Math.random() * 255);
                    var r3 = Math.floor(Math.random() * 255);

                    for (var i = 0; i < 320; i++) {
                        var tmp = parseInt(col[i]);

                        /* Copied some xcolor methods here to be faster than calling all methods inside of xcolor and to not serve complete library with every req */

                        if (id == 0) {
                            r = (tmp >> 16) & 0xff;
                            g = 0xff;
                            b = 0xff;
                        } else if (id == 1) {
                            r = 0xff;
                            g = (tmp >> 8) & 0xff;
                            b = 0xff;
                        } else if (id == 2) {
                            r = 0xff;
                            g = 0xff;
                            b = tmp & 0xff;
                        } else if (id == 3) {
                            r = 0xff ^ ((tmp >> 16) & 0xff);
                            g = 0xff ^ ((tmp >> 8) & 0xff);
                            b = 0xff ^ (tmp & 0xff);
                        } else if (id == 4) {

                            r = (tmp >> 16) & 0xff;
                            g = (tmp >> 8) & 0xff;
                            b = tmp & 0xff;
                            var v = Math.min(Math.floor(.35 + 13 * (r + g + b) / 60), 255);
                            r = v;
                            g = v;
                            b = v;
                        } else if (id == 5) {
                            r = (tmp >> 16) & 0xff;
                            g = (tmp >> 8) & 0xff;
                            b = tmp & 0xff;
                            if ((r += 32) < 0) r = 0;
                            if ((g += 32) < 0) g = 0;
                            if ((b += 32) < 0) b = 0;
                        } else if (id == 6) {
                            r = (tmp >> 16) & 0xff;
                            g = (tmp >> 8) & 0xff;
                            b = tmp & 0xff;
                            if ((r -= 32) < 0) r = 0;
                            if ((g -= 32) < 0) g = 0;
                            if ((b -= 32) < 0) b = 0;
                        } else if (id == 7) {
                            r = (tmp >> 16) & 0xff;
                            g = (tmp >> 8) & 0xff;
                            b = tmp & 0xff;
                            r = Math.floor(r / 255 * r1);
                            g = Math.floor(g / 255 * r2);
                            b = Math.floor(b / 255 * r3);
                        }

                        img.data[pos + 0] = r;
                        img.data[pos + 1] = g;
                        img.data[pos + 2] = b;
                        img.data[pos + 3] = 0xff;
                        pos += 4;
                    }
                }

                if (pos >= 0x4B000) {
                    ctx.putImageData(img, 0, 0);
                    pos = 0;
                    var canvas = document.getElementById("canvas");
                    //  $.post("http://192.168.1.199/HaomaTesting/WebCam/UploadImage.aspx", { image: canvas.toDataURL("image/png") });

                }
            },

            onCapture: function () {

                jQuery("#flash").css("display", "block");
                jQuery("#flash").fadeOut(100, function () {
                    jQuery("#flash").css("opacity", 1);
                });
                webcam.save();
            },

            debug: function (type, string) {

                jQuery("#status").html(type + ": " + string);

            },

            onLoad: function () {

                var cams = webcam.getCameraList();
                for (var i in cams) {
                    jQuery("#cams").append("<li>" + cams[i] + "</li>");
                }
            }

        }

);

        function getPageSize() {

            var xScroll, yScroll;

            if (window.innerHeight && window.scrollMaxY) {
                xScroll = window.innerWidth + window.scrollMaxX;
                yScroll = window.innerHeight + window.scrollMaxY;
            } else if (document.body.scrollHeight > document.body.offsetHeight) { // all but Explorer Mac
                xScroll = document.body.scrollWidth;
                yScroll = document.body.scrollHeight;
            } else { // Explorer Mac...would also work in Explorer 6 Strict, Mozilla and Safari
                xScroll = document.body.offsetWidth;
                yScroll = document.body.offsetHeight;
            }

            var windowWidth, windowHeight;

            if (self.innerHeight) { // all except Explorer
                if (document.documentElement.clientWidth) {
                    windowWidth = document.documentElement.clientWidth;
                } else {
                    windowWidth = self.innerWidth;
                }
                windowHeight = self.innerHeight;
            } else if (document.documentElement && document.documentElement.clientHeight) { // Explorer 6 Strict Mode
                windowWidth = document.documentElement.clientWidth;
                windowHeight = document.documentElement.clientHeight;
            } else if (document.body) { // other Explorers
                windowWidth = document.body.clientWidth;
                windowHeight = document.body.clientHeight;
            }

            // for small pages with total height less then height of the viewport
            if (yScroll < windowHeight) {
                pageHeight = windowHeight;
            } else {
                pageHeight = yScroll;
            }

            // for small pages with total width less then width of the viewport
            if (xScroll < windowWidth) {
                pageWidth = xScroll;
            } else {
                pageWidth = windowWidth;
            }
            return [pageWidth, pageHeight];
        }


        window.addEventListener("load", function () {

            jQuery("body").append("<div id=\"flash\"></div>");

            var canvas = document.getElementById("canvas");

            if (canvas && canvas.getContext) {
                //ctx = document.getElementById("canvas").getContext("2d");
                ctx = canvas.getContext("2d");
                if (ctx) {
                    ctx.clearRect(0, 0, 320, 240);

                    var img = new Image();
                    img.src = "/static/logo.gif";

                    img.onload = function () {
                        ctx.drawImage(img, 129, 89);
                    }
                    image = ctx.getImageData(0, 0, 320, 240);
                }

            }

            var pageSize = getPageSize();

            jQuery("#flash").css({ height: pageSize[1] + "px" });

        }, false);

        window.addEventListener("resize", function () {

            var pageSize = getPageSize();

            jQuery("#flash").css({ height: pageSize[1] + "px" });

        }, false);


        function UploadPic() {
            //            debugger;
            // generate the image data

            var canvas = document.getElementById("canvas");
            var dataURL = canvas.toDataURL("image/png");

            // Sending the image data to Server
            $.ajax({
                type: 'POST',
                url: "baseimg.aspx?codigovisitante=" + cod_visi,
                data: { imgBase64: dataURL },
                success: function () {
                    //alert("Listo!!, Foto Almacenada.");
                    //                    window.opener.location.reload(true); // reloading Parent page
                    top.opener.Refrescar();
                    window.close();
                    //                    window.opener.setVal(1);

                    return false;
                }
            });
        }


    </script>
</body>
</html>
