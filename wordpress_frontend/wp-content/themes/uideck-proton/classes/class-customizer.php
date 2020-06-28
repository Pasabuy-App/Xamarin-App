<?php
    /**
     * Customizer settings for this theme.
     *
     * @package uideck-proton
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

