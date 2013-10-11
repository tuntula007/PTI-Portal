<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>Petroleum Training Institute,Effurun, Delta State</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <!-- Le styles -->
    <link href="css/bootstrap.css" rel="stylesheet">
    <link href="css/docs.css" rel="stylesheet">
    <link href="css/bootstrap-responsive.css" rel="stylesheet">
    <style type="text/css">
        body
        {
            padding-top: 60px;
            padding-bottom: 40px;
        }
        .sidebar-nav
        {
            padding: 9px 0;
        }
        @media (max-width: 980px)
        {
            /* Enable use of floated navbar text */    .navbar-text.pull-right
            {
                float: none;
                padding-left: 5px;
                padding-right: 5px;
            }
        }
    </style>
    <style>
        $(
        function()
        { $('.carousel').carousel({
      interval: 2000
    });
$('.carousel-control.right').trigger('click');
});</style>
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="../assets/js/html5shiv.js"></script>
    <![endif]-->
    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="../assets/ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="../assets/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="../assets/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="../assets/ico/apple-touch-icon-57-precomposed.png">
    <link href="/img/PTI-Logo.jpg" rel="shortcut icon" type="image/x-icon" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container-fluid">
                <button type="button" class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
                    <span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar">
                    </span>
                </button>
                <div class="nav-collapse collapse">
                    <ul class="nav">
                        <li class="active"><a href="Default.aspx">Home</a></li>
                        <li><a href="StudentLogin.aspx">Student Login</a></li>
                        <li><a href="#">FAQs</a></li>
                        <li><a href="ContactUs.aspx">Contact Us</a> </li>
                        <li><a href="http://mail.pti.edu.ng ">Staff Email</a> </li>
                        <li><a href="Downloads.aspx">Downloads</a> </li>
                    </ul>
                </div>
                <div>
                    <a class="logo" href="#">
                        <img src="img/PTI-Logo-smalL.fw.png">
                    </a>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span3">
                <div class="well sidebar-nav">
                    <ul class="nav nav-list">
                        <!--<li class="nav-header">HOT LINKS</li>-->
                        <li><a class="sidebar" href="ApplicationProcedure.aspx">Application Procedure &raquo;</a></li>
                        <li><a class="sidebar" href="ApplicationInfo.aspx">Available Programmes &raquo;</a></li>
                        <li><a class="sidebar" href="ApplicantsSignOn.aspx">Apply For Admission &raquo;</a></li>
                        <li><a class="sidebar" href="ApplicantLogin.aspx">Return To My Application &raquo;</a></li>
                        <li><a class="sidebar" href="Prospectus 2013.pdf">Download Prospectus &raquo;</a></li>
                        <li><a class="sidebar" href="PTI Short courses for 2013 year.pdf">Download Short Courses
                            &raquo;</a></li>
                        <li><a class="sidebar" href="Charges for 2013 Short Courses.pdf">Short Courses Fees
                            &raquo;</a></li>
                        <li><a class="sidebar" href="ApplicantLogin.aspx">Check Exam Scores &raquo;</a></li>
                        <li><a class="sidebar" href="ApplicantLogin.aspx">Check Admission Status &raquo;</a></li>
                        <li><a class="sidebar" href="StudentLogin.aspx">Course Registration &raquo;</a></li>
                        <li><a class="sidebar" href="#">E-Library &raquo;</a></li>
                    </ul>
                    <br>
                    <br>
                    <div align="center">
                        <a href="#">
                            <img src="img/Faqsmain1.jpg"></a></div>
                </div>
                <!--/.well -->
            </div>
            <!--/span-->
            <div class="span8">
                <div class="hero-unit bs-docs-example">
                    <div id="myCarousel" class="carousel slide auto">
                        <!-- Carousel items -->
                        <div class="carousel-inner">
                            <div class="item active">
                                <div>
                                    <asp:Image Width="100%" Height="330px" ID="Image1" runat="server" ImageUrl="img/entrance_exams.jpg" />
                                    <div class="carousel-caption">
                                        <h2>
                                            Welcome To Petroleum Training Institute, Effurun, Delta State, Nigeria
                                        </h2>
                                        <p>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <asp:SqlDataSource ID="drscNews" runat="server" ConnectionString="<%$ ConnectionStrings:PTIEduportalConnectionString %>"
                                
                                SelectCommand="SELECT * FROM [NewsItems] WHERE ([Active] = @Active) ORDER BY [NewsId] DESC">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="True" Name="Active" Type="Boolean" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:Repeater ID="rptNews" runat="server" DataSourceID="drscNews">
                                <ItemTemplate>
                                    <div class="item">
                                        <div>
                                            <asp:Image Width="100%" Height="330px" ID="Image1" runat="server" ImageUrl='<%# Eval("ImagePath")%>' />
                                            <div class="carousel-caption">
                                                <h2>
                                                    <%# Eval("Title")%>
                                                </h2>
                                                <p>
                                                    <%# Eval("Caption")%>
                                                    <a href='<%# "HomePageNews.aspx?NewsId=" + Eval("NewsId")%>'>Click Here</a>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            
                        </div>
                        <!-- Carousel nav -->
                        <a class="carousel-control left" href="#myCarousel" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#myCarousel" data-slide="next">&rsaquo;</a>
                    </div>
                </div>
                <div class="row-fluid">
                    <li class="span4">
                        <div class="thumbnail">
                            <img data-src="holder.js/300x200" alt="">
                            <h3>
                                Our Vision</h3>
                            <p>
                                " TO BECOME THE LEADING
                                <br>
                                OIL AND GAS<br>
                                TECHNOLOGICAL ... INSTITUTE
                                <br>
                                IN AFRICA"
                                <br>
                                <br>
                                <br>
                            </p>
                        </div>
                    </li>
                    <li class="span4">
                        <div class="thumbnail">
                            <img data-src="holder.js/300x200" alt="">
                            <h3 class="notices">
                                Important Notice</h3>
                            <p>
                                <strong>For Admission Enquiries;</strong><br>
                                admission@pti.edu.ng , 08032640578 <strong>For Portal Complaints;</strong> itsupport@pti.edu.ng
                                , 08126971127, 08052742813
                                <br>
                                <strong>All Calls must be between;<br>
                                    Mon-Fri, 9am-4pm.</strong></p>
                        </div>
                    </li>
                    <li class="span4">
                        <div class="thumbnail">
                            <img data-src="holder.js/300x200" alt="">
                            <h3>
                                Our Mission</h3>
                            <p>
                                " TO PROVIDE COMPETENT TECHNOLOGICAL MANPOWER THROUGH QUALITY TRAINING, RESEARCH,
                                AND CONSULTANCY FOR THE PETROLEUM AND ALLIED INDUSTRIES."
                                <br>
                                <br>
                            </p>
                        </div>
                    </li>
                </div>
                <div class="row-fluid">
                    <div class="span6">
                        <h3>
                            Returning Student</h3>
                        <p>
                            <strong>Follow the steps below to do your session clearance:</strong><br>
                            • Pay for PTI School Fees at the participating bank listed below and obtain a receipt
                            containing your Confirmation Order Number<br>
                            • If you are Old/Returning Student who is new to the Portal kindly <a href="StudentLogin.aspx">click
                                here</a> to continue<br>
                            • If you have carried out the above step, kindly <a href="StudentLogin.aspx">click</a> here to continue.
                        </p>
                        <p>
                            <a class="btn" href="#">View details &raquo;</a></p>
                    </div>
                    <!--/span-->
                    <div class="span6">
                        <h3>
                            Registration of Courses</h3>
                        <p>
                            <strong>New and returning students Continuing with your course registration requires
                                you to have done the following:</strong><br>
                            • Pay for PTI School Fees at the participating bank and obtain a receipt containing
                            your Confirmation Order Number<br>
                            • If you have carried out the above steps, kindly click on <a href="StudentLogin.aspx">[REGISTER]</a>
                            to continue.
                        </p>
                        <p>
                            <a class="btn" href="#">View details &raquo;</a></p>
                    </div>
                    <!--/span-->
                </div>
            </div>
            <!--/row-->
        </div>
        <!--/span-->
    </div>
    <!--/row-->
    <hr>
    <footer>
        <p> Copyright &copy; 2011 - <% Response.Write(DateTime.Now.Year.ToString()); %>. Petroleum Training Institute, Effurun, Delta State</p>
      </footer>
    </div><!--/.fluid-container-->
    <!-- Le javascript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->

    <script src="js/jquery.js" type="text/javascript"></script>

    <script src="js/bootstrap-transition.js" type="text/javascript"></script>

    <script src="js/bootstrap-alert.js" type="text/javascript"></script>

    <script src="js/bootstrap-modal.js" type="text/javascript"></script>

    <script src="js/bootstrap-dropdown.js" type="text/javascript"></script>

    <script src="js/bootstrap-scrollspy.js" type="text/javascript"></script>

    <script src="js/bootstrap-tab.js" type="text/javascript"></script>

    <script src="js/bootstrap-tooltip.js" type="text/javascript"></script>

    <script src="js/bootstrap-popover.js" type="text/javascript"></script>

    <script src="js/bootstrap-button.js" type="text/javascript"></script>

    <script src="js/bootstrap-collapse.js" type="text/javascript"></script>

    <script src="js/bootstrap-carousel.js" type="text/javascript"></script>

    <script src="js/bootstrap-typeahead.js" type="text/javascript"></script>

    <script type="text/javascript">
        $('.carousel').carousel({
            interval: 4000
        })
    </script>

    </form>
</body>
</html>
