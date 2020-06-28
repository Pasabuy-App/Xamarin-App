<?php
	/**
	 * UIDECK Proton blocks for blog list.
	 *
	 * @package uideck-proton
	 * @since 0.1.0
	 */
?>

    <!-- download Section Start -->
    <section id="download">
      <div class="container">
        <div class="row">
          <div class="col-lg-6 col-md-6 col-xs-12">            
            <div class="download-thumb wow fadeInLeft" data-wow-delay="0.2s">
              <img class="img-fluid" src="<?php echo get_template_directory_uri(); ?>/assets/img/mac.png" alt="">
            </div>
          </div>
          <div class="col-lg-6 col-md-6 col-xs-12">
            <div class="download-wrapper wow fadeInRight" data-wow-delay="0.2s">
              <div>
                <div class="download-text">
                  <h4>Join our growing Community of Users</h4>
                  <p>We make sure that our availability on all the platforms available on the market is supported. Aside from this two major medium for our app, we are still looking for a potential platforms to have our application deployed.</p>
                </div>
                <a href="#" class="btn btn-common btn-effect"><i class="lni-android"></i> PlayStore<br></a>
                <a href="#" class="btn btn-common btn-effect"><i class="lni-apple"></i> AppStore<br></a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <!-- download Section Start -->

    <!-- Blog Section -->
    <section id="blog" class="section">
      <!-- Container Starts -->
      <div class="container">
         <div class="section-header">   
          <p class="btn btn-subtitle wow fadeInDown" data-wow-delay="0.2s">Advisory</p>       
          <h2 class="section-title">Newest Announcements</h2>
        </div>
        <div class="row">

            <?php
                $the_query = new WP_Query( array(
                    'posts_per_page' => 3,
                ));
                if ( $the_query->have_posts() ) :
                    while ( $the_query->have_posts() ) : $the_query->the_post(); 
            ?>

                <div class="col-lg-4 col-md-6 col-xs-12 blog-item">
                    <!-- Blog Item Starts -->
                    <div class="blog-item-wrapper">
                    <div class="blog-item-img">
                        <a href="<?php the_permalink(); ?>">
                        <img src="<?php echo getFeaturedImage(get_the_ID(), 'blog-list-thumbnail'); ?>" alt="">
                        </a>   
                        <div class="author-img">
                            <img src="<?php echo getAvatarImage( $post->post_author ); ?>" alt="">
                        </div>             
                    </div>
                    <div class="blog-item-text"> 
                        <h3><a href="<?php the_permalink(); ?>"><?php the_title(); ?></a></h3>
                        <div class="author">
                        <span class="name"><a href="<?php echo get_the_author_meta( 'user_url' , $post->post_author ); ?>"><?php echo get_the_author(); ?></a></span>
                        <span class="date float-right"><?php echo get_the_date(); ?></span>
                        </div>
                    </div>
                    </div>
                    <!-- Blog Item Wrapper Ends-->
                </div>

            <?php
                    endwhile;
                    wp_reset_query();
                endif;
            ?>

        </div>
      </div>
    </section>
    <!-- blog Section End -->