
<?php
	// Exit if accessed directly
	if ( ! defined( 'ABSPATH' ) ) 
	{
		exit;
	}

	/** 
		* @package pasabuy-plugin
		* Name: PasaBuy Plugin
		* Description: WordPress plugin for PasaBuy App owned by PasaBuy Tech.
		* Package-Website: https://www.bytescrafter.net/projects/pasabuy-plugin
		* 
		* Author: Bytes Crafter
		* Author-Website:: https://www.bytescrafter.net/about-us
		* License: Copyright (C) Bytes Crafter - All rights Reserved. 
	*/
?>

<?php

    //Require the USocketNet class which have the core function of this plguin. 
    require plugin_dir_path(__FILE__) . '/v1/class-user-auth.php';
    require plugin_dir_path(__FILE__) . '/v1/class-user-verify.php';

    //require plugin_dir_path(__FILE__) . '/class-demoguy.php';
    include_once( plugin_dir_path( __FILE__ ) . '/v1/demo-callback.php' );

    // Init check if PasaBuy successfully request from wapi.
    function bytescrafter_pasabuy_route()
    {
        register_rest_route( 'pasabuy/v1/user', 'auth', array(
            'methods' => 'POST',
            'callback' => array('PSP_Authenticate','initialize'),
        ));

        register_rest_route( 'pasabuy/v1/user', 'verify', array(
            'methods' => 'POST',
            'callback' => array('PSP_Verification','initialize'),
        ));
    }
    add_action( 'rest_api_init', 'bytescrafter_pasabuy_route' );

?>