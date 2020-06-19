
<?php
	/**
    * The template file used to render a static page (page post-type). Note that 
    * unlike other post-types, page is special to WordPress and uses the following path:
	*
	* @package pasabuy
	* @since 0.1.0
	*/
?>

<?php get_header(); ?>

	<?php
		while ( have_posts() ) : the_post(); 
	?>
	<div style="background: url(<?php getPostFeaturedImage($post->ID, 'featured-image'); ?>) no-repeat center; background-size: cover; height: 70vh; margin-bottom: 70px;">
	</div>
	<div class="container">
    	<div class="row">
			<div class="col-md-8">
				<section class="section section-padding">
						<div class="short-info">
							<?php the_content(); ?>
						</div>
					<?php
						endwhile;
						wp_reset_query();
					?>
				</section>

				<?php
					if ( ( is_single() || is_page() ) && ( comments_open() || get_comments_number() ) && ! post_password_required() ) {
				?>
				
						<!-- <div class="comments-wrapper section-inner">
							<?php //comments_template(); ?>
						</div> -->

				<?php
					}
				?>
			</div>

			<div class="col-md-4" id="sidebar">
				<?php get_sidebar( 'primary' );; ?>
			</div>

		</div>
    </div>

<?php get_footer(); ?>