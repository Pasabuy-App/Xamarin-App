<?php
	/**
	* The template for displaying 404 pages (not found)
	*
	* @package uideck-proton
	* @since 0.1.0
	*/
?>

<?php get_header(); ?>

	<!-- <div style="background: url(<?php //getPostFeaturedImage('', ''); ?>) no-repeat center; background-size: cover; height: 40vh; margin-bottom: 70px;">
	</div> -->

	<section id="primary" class="content-area">
		<main id="main" class="site-main">

			<div class="error-404 not-found" style="text-align: center; margin: 75px 0;">
				<img class="illustration-404" src="<?php echo get_template_directory_uri()."/assets/img/features/img2.png"; ?>" alt="">
				<header class="page-header">
					<h1 class="page-title"><?php _e( 'Looking for Something?', 'uideck-proton' ); ?></h1>
				</header>

				<div class="page-content">
					<p><?php _e( 'It looks like nothing was found at this location.', 'uideck-proton' ); ?></p>
				</div>
			</div>

		</main>
	</section>

<?php get_footer(); ?>