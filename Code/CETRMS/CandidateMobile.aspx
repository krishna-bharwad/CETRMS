<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CandidateMobile.aspx.cs" Inherits="CETRMS.CandidateMobile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>



    <style>

    .main{
        display: flex;
        align-items: center;
        justify-content: center;
        height: 100vh;
        width: 100vw;
        /*border: 1px solid black;*/
        position: relative;
        /*overflow: hidden;*/
        background-image: url("IndexAssets/images/MobileAppImg.png");
        background-repeat: no-repeat;
        background-size: cover;
    }

    /*.bgImg{
        border: 2px solid red;
        height: 100vh;
        width: 100%;

    }
*/

    .content{
        display: flex;
        align-items:center;
        justify-content: space-between;
        /*border: 1px solid black;*/
        height: 80%;
        width: 80%;
        margin-top: -5em;
        padding: 1.2em;
    }


    .mobileAppImg{
        /*border: 1px solid red;*/      
        height: 100%;
      
    }

    .mobileImg{
        position: relative;
        margin-top: -3em;
        height: 100%;
        width: 100%;
    }


    .mobileAppImg-text{
        display: flex;
        flex-wrap: wrap;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        /*border: 1px solid red;*/
        margin-top: -6em;
        color: black;
        font-weight: bold;
    }

    .download-btn{
        height: 55px;
        width: 150px;
        border: 1px solid white;
        /*border-radius: 55px;*/
        background-color: transparent;
        color: white;
        transition: all ease-in;
    }

    .download-btn:hover{
         background-color: white;
        color: #66acac ;
    }

   

    </style>





</head>
<body>
    <form id="form1" runat="server">
        <div class="main">

            <%--<img class="bgImg" src="IndexAssets/images/MobileAppImg.png"/>--%>

            <div class="content">


                <div class="mobileAppImg-text">
                    <h1>Download APK for Candidate</h1>

                    <p></p>

                    <asp:Button CssClass="download-btn" ID="IOS" runat="server" Text="Dpwnload APK" />
                    <%--<asp:Button CssClass="download-btn" ID="Android" runat="server" Text="Download APk"/>--%>

                </div>


                <div class="mobileAppImg">
                    <img class="mobileImg" src="IndexAssets/images/Apple iPhone SE Screenshot 1.png" />
                </div>
                


            </div>


        </div>


    </form>
</body>
</html>
