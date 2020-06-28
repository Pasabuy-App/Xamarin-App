<?php
    /**
     * Customizer settings for this theme.
     *
     * @package uideck-proton
     * @since 0.1.0
     */
?>

<?php

    //Return a string of home url, if not exist use default.
    function getSingleMenuItem($redirect) {
        $tarField = $redirect;
        if( !is_front_page() ) {
            $tarField = home_url() . "/" . $redirect;
        }
        echo $tarField;
    }

    //Get avatar default if not set by id.
    function getAvatarImage( $userId ) {   
        $avatar_image_url = get_avatar_url($userId, array('size' => 120));

        if ( strpos($avatar_image_url, 'www.gravatar.com/avatar/') !== false ) {
            $avatar_image_url = get_template_directory_uri()."/assets/img/blog/author.png"; 
        }

        $avatar_image_url = str_replace(home_url(), '', $avatar_image_url);

        echo $avatar_image_url;
    }

    //Get featured image of a post by id.
    function getFeaturedImage( $postId, $sizeGroup ) {   
        $returningImage = get_template_directory_uri()."/assets/img/blog/img1.jpg"; 

        if ( $postId != '' ) {
            if ( has_post_thumbnail( $postId ) ) {
                $imageAttachment = wp_get_attachment_image_src( get_post_thumbnail_id( $postId ), $sizeGroup );
                if( !empty($imageAttachment) ) {
                    $returningImage = $imageAttachment[0];
                }
            }
        }

        echo $returningImage;
    }

    //Return a string of theme mod, if not exist use default.
    function getThemeField($key, $default) {
        $tarField = get_theme_mod( $key );
        if( empty($tarField) ) {
            $tarField = $default;
        } else {
            $tarField = $tarField;
        }
        echo $tarField;
    }

    //Get custom logo of a post by id.
    // add_image_size('featured', 370, 240, true); on function theme support.
    function getCustomLogo() {   
        $returningImage = get_template_directory_uri()."/assets/img/logo.png"; 

        $custom_logo_id = get_theme_mod( 'custom_logo' );
        $image = wp_get_attachment_image_src( $custom_logo_id , 'header-icon-size' );
        if( !empty($image) ) {
            $returningImage = $image[0];
        }

        echo $returningImage;
    }

    //adding setting for Footer text
    function uid_social_link_customizer($wp_customize) {

        //adding section in wordpress customizer   
        $wp_customize->add_section('social_links_section', array(
            'title' => 'Social Links'
        ));

        //adding setting facebook
        $wp_customize->add_setting('uid_social_fb', array(
            'default'        => 'https://www.facebook.com',
            'transport' => 'refresh',
        )); $wp_customize->add_control('uid_social_fb', array(
            'label'   => 'Facebook',
            'section' => 'social_links_section',
            'type'    => 'text',
            'description' => __('Put Facebook page url here: {uid_social_fb}', 'facebook' ),
            'input_attrs' => array(
                'placeholder' => __( 'https://www.facebook.com/BytesCrafterPH', 'facebook' ),
            )
        ));

        //adding setting twitter
        $wp_customize->add_setting('uid_social_tw', array(
            'default'        => 'https://www.twitter.com',
            'transport' => 'refresh',
        )); $wp_customize->add_control('uid_social_tw', array(
            'label'   => 'Twitter',
            'section' => 'social_links_section',
            'type'    => 'text',
            'description' => __('Put Twitter account url here: {uid_social_tw}', 'twitter' ),
            'input_attrs' => array(
                'placeholder' => __( 'https://twitter.com/BytesCrafter', 'twitter' ),
            )
        ));

        //adding setting youtube
        $wp_customize->add_setting('uid_social_yt', array(
            'default'        => 'https://www.youtube.com',
            'transport' => 'refresh',
        )); $wp_customize->add_control('uid_social_yt', array(
            'label'   => 'YouTube',
            'section' => 'social_links_section',
            'type'    => 'text',
            'description' => __('Put your YouTube channel here: {uid_social_yt}', 'youtube' ),
            'input_attrs' => array(
                'placeholder' => __( 'https://www.youtube.com/channel/UCHXZUImmr9aSKmYpKXqN9vQ', 'youtube' ),
            )

        ));

        //adding setting linkedin
        $wp_customize->add_setting('uid_social_li', array(
            'default'        => 'https://www.linkedin.com',
            'transport' => 'refresh',
        )); $wp_customize->add_control('uid_social_li', array(
            'label'   => 'Linkedin',
            'section' => 'social_links_section',
            'type'    => 'text',
            'description' => __('Put your LinkedIn profile here: {social_li}', 'linkedin' ),
            'input_attrs' => array(
                'placeholder' => __( 'https://play.google.com/store/apps/dev?id=5394145917362507576', 'linkedin' ),
            )

        ));

    } add_action('customize_register', 'uid_social_link_customizer');

