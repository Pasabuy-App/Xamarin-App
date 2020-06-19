    
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