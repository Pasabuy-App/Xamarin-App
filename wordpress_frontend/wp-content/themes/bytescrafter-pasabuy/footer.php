<?php
	/**
	* The template for displaying the header
	*
	* @package pasabuy
	* @since 0.1.0
	*/

?>

        <!-- Footer -->
        <div class="footer">
            <div class="container">
                <div class="row">
                    <div class="col-md-4">
                        <div class="footer-col">
                            <h4>About PasaBuy App</h4>
                            <p>We're passionate about offering some of the best business growth services for startups.</p>
                        </div>
                    </div> <!-- end of col -->
                    <div class="col-md-4">
                        <div class="footer-col middle">
                            <h4>Important Links</h4>
                            <ul class="list-unstyled li-space-lg">
                                <li class="media">
                                    <i class="fas fa-square"></i>
                                    <div class="media-body">Our business partners <a class="turquoise" href="#your-link">Marketplace</a></div>
                                </li>
                                <li class="media">
                                    <i class="fas fa-square"></i>
                                    <div class="media-body">Read our <a class="turquoise" href="<?php echo site_url() . '/privacy-policy'; ?>">Terms & Conditions</a>, <a class="turquoise" href="<?php echo site_url() . '/terms-and-conditions'; ?>">Privacy Policy</a></div>
                                </li>
                            </ul>
                        </div>
                    </div> <!-- end of col -->
                    <div class="col-md-4">
                        <div class="footer-col last">
                            <h4>Social Media</h4>
                            <?php if(!empty(getThemeField( 'social_fb', '' ))) { ?>
                                <span class="fa-stack">
                                    <a href="<?php echo getThemeField( 'social_fb', '#' ); ?>">
                                        <i class="fas fa-circle fa-stack-2x"></i>
                                        <i class="fab fa-facebook-f fa-stack-1x"></i>
                                    </a>
                                </span>
                            <?php } ?>
                            <?php if(!empty(getThemeField( 'social_fb', '' ))) { ?>
                                <span class="fa-stack">
                                    <a href="<?php echo getThemeField( 'social_tw', '#' ); ?>">
                                        <i class="fas fa-circle fa-stack-2x"></i>
                                        <i class="fab fa-twitter fa-stack-1x"></i>
                                    </a>
                                </span>
                            <?php } ?>
                            <?php if(!empty(getThemeField( 'social_fb', '' ))) { ?>
                                <span class="fa-stack">
                                    <a href="<?php echo getThemeField( 'social_yt', '#' ); ?>">
                                        <i class="fas fa-circle fa-stack-2x"></i>
                                        <i class="fab fa-youtube fa-stack-1x"></i>
                                    </a>
                                </span>
                            <?php } ?>
                            <?php if(!empty(getThemeField( 'social_fb', '' ))) { ?>
                                <span class="fa-stack">
                                    <a href="<?php echo getThemeField( 'social_li', '#' ); ?>">
                                        <i class="fas fa-circle fa-stack-2x"></i>
                                        <i class="fab fa-linkedin-in fa-stack-1x"></i>
                                    </a>
                                </span>
                            <?php } ?>
                        </div> 
                    </div>
                </div>
            </div>
        </div>

        <!-- Copyright -->
        <div class="copyright">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <p class="p-small">Copyright © <?php echo Date('Y'); ?> <a href="https://pasabuy.app">PasaBuy Tech</a> - All rights reserved</p>
                    </div>
                </div>
            </div>
        </div>

    </body>
</html>