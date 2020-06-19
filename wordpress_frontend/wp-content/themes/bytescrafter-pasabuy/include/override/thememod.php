    
    <?php
        /**
        * Theme Core - Theme Mod
        *
        * @package pasabuy
        * @since 0.1.0
        */
    ?>

<?php

    //Return a string of theme mod, if not exist use default.
    function getThemeField($key, $default) {
        $tarField = get_theme_mod( $key );
        if( empty($tarField) ) {
            return $default;
        } else {
            return $tarField;
        }
        echo $tarField;
    }

    //Get featured image of a post by id.
    function getPostFeaturedImage( $postId, $sizeGroup ) {   
        $returningImage = get_template_directory_uri()."/assets/images/default-thumbnail.png"; 

        if ( has_post_thumbnail( $postId ) ) {
            $imageAttachment = wp_get_attachment_image_src( get_post_thumbnail_id( $postId ), $sizeGroup );
            if( !empty($imageAttachment) ) {
                $returningImage = $imageAttachment[0];
            }
        }

        echo $returningImage;
    }

    function ps_register_sidebars() {
        register_sidebar(
            array(
                'id'            => 'primary',
                'name'          => __( 'Primary Sidebar' ),
                'description'   => __( 'Put all content that you want to show in this site wide sidebar.' ),
                'before_widget' => '<div id="%1$s" class="widget %2$s">',
                'after_widget'  => '</div>',
                'before_title'  => '<h3 class="widget-title">',
                'after_title'   => '</h3>',
            )
        );
    }
    add_action( 'widgets_init', 'ps_register_sidebars' );