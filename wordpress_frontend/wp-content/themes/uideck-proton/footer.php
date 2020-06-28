<?php
	/**
	* The template for displaying the header
	*
	* @package uideck-proton
	* @since 0.1.0
	*/
?>

    <!-- Footer Section Start -->
    <footer>
      <!-- Footer Area Start -->
      <section class="footer-Content">
        <div class="container">
          <div class="row">
            <div class="col-lg-3 col-md-6 col-sm-6 col-xs-6 col-mb-12">
              <img src="<?php echo get_template_directory_uri(); ?>/assets/img/logo.png" alt="">
              <div class="textwidget">
                <p>Appropriately implement one-to-one catalysts for change vis-a-vis wireless catalysts for change. Enthusiastically architect adaptive.</p>
              </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-6 col-xs-6 col-mb-12">
              <div class="widget">
                <h3 class="block-title">Create a Free Account</h3>
                <ul class="menu">
                  <li><a href="#">Sign In</a></li>
                  <li><a href="#">About Us</a></li>
                  <li><a href="#">Pricing</a></li>
                  <li><a href="#">Jobs</a></li>
                </ul>
              </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-6 col-xs-6 col-mb-12">
              <div class="widget">
                <h3 class="block-title">Resource</h3>
                <ul class="menu">
                  <li><a href="#">Comunnity</a></li>
                  <li><a href="#">Become a Partner</a></li>
                  <li><a href="#">Our Technology</a></li>
                  <li><a href="#">Documentation</a></li>
                </ul>
              </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-6 col-xs-6 col-mb-12">
              <div class="widget">
                <h3 class="block-title">Support</h3>
                <ul class="menu">
                  <li><a href="#">Terms & Condition</a></li>
                  <li><a href="#">Contact Us</a></li>
                  <li><a href="#">Privacy Policy</a></li>
                  <li><a href="#">Help</a></li>
                </ul>
              </div>
            </div>
          </div>
        </div>
        <!-- Copyright Start  -->
        <div class="copyright">
          <div class="container">
            <div class="row">
              <div class="col-md-12">
                <div class="site-info float-left">
                  <p>Copyright &copy; 2020 - Powered by <a href="http://bytescrafter.net" rel="nofollow">Bytes Crafter</a></p>
                </div>              
                <div class="float-right">  
                  <ul class="footer-social">
                    <?php if(!empty(getThemeField( 'uid_social_fb', '' ))) { ?>
                      <li><a class="facebook" href="<?php echo getThemeField( 'uid_social_fb', '#' ); ?>"><i class="lni-facebook-filled"></i></a></li>
                    <?php } ?>
                    <?php if(!empty(getThemeField( 'uid_social_tw', '' ))) { ?>
                      <li><a class="twitter" href="<?php echo getThemeField( 'uid_social_tw', '#' ); ?>"><i class="lni-twitter-filled"></i></a></li>
                    <?php } ?>
                    <?php if(!empty(getThemeField( 'uid_social_yt', '' ))) { ?>
                      <li><a class="linkedin" href="<?php echo getThemeField( 'uid_social_yt', '#' ); ?>"><i class="lni-playstore"></i></a></li>
                    <?php } ?>
                    <?php if(!empty(getThemeField( 'uid_social_li', '' ))) { ?>
                      <li><a class="google-plus" href="<?php echo getThemeField( 'uid_social_li', '#' ); ?>"><i class="lni-linkedin-fill"></i></a></li>
                    <?php } ?>
                  </ul> 
                </div>
              </div>
            </div>
          </div>
        </div>
      <!-- Copyright End -->
      </section>
      <!-- Footer area End -->
      
    </footer>
    <!-- Footer Section End --> 

    <!-- Go To Top Link -->
    <a href="#" class="back-to-top">
      <i class="lni-chevron-up"></i>
    </a> 

    <!-- Preloader -->
    <div id="preloader">
      <div class="loader" id="loader-1"></div>
    </div>
    <!-- End Preloader -->

    <!-- jQuery first, then Tether, then Bootstrap JS. -->
    <script src="<?php echo get_template_directory_uri(); ?>/assets/js/jquery-min.js"></script>
    <script src="<?php echo get_template_directory_uri(); ?>/assets/js/popper.min.js"></script>
    <script src="<?php echo get_template_directory_uri(); ?>/assets/js/bootstrap.min.js"></script>
    <script src="<?php echo get_template_directory_uri(); ?>/assets/js/owl.carousel.js"></script>

    <script src="<?php echo get_template_directory_uri(); ?>/assets/js/jquery.mixitup.js"></script>
    <script src="<?php echo get_template_directory_uri(); ?>/assets/js/jquery.nav.js"></script>
    <script src="<?php echo get_template_directory_uri(); ?>/assets/js/scrolling-nav.js"></script>
    <script src="<?php echo get_template_directory_uri(); ?>/assets/js/jquery.easing.min.js"></script>
    <script src="<?php echo get_template_directory_uri(); ?>/assets/js/wow.js"></script>

    <script src="<?php echo get_template_directory_uri(); ?>/assets/js/jquery.counterup.min.js"></script>
    <script src="<?php echo get_template_directory_uri(); ?>/assets/js/nivo-lightbox.js"></script>
    <script src="<?php echo get_template_directory_uri(); ?>/assets/js/jquery.magnific-popup.min.js"></script>
    <script src="<?php echo get_template_directory_uri(); ?>/assets/js/waypoints.min.js"></script>
    <!-- <script src="<?php //echo get_template_directory_uri(); ?>/assets/js/form-validator.min.js.js"></script> -->
    <!-- <script src="<?php //echo get_template_directory_uri(); ?>/assets/js/contact-form-script.js"></script> -->
    <script src="<?php echo get_template_directory_uri(); ?>/assets/js/script.js"></script>
    
  </body>
</html>