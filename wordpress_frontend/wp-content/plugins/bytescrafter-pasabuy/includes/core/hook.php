
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

    /**
     * Activation hook.
     */
    function usocketnet_activate() {
        global $wpdb;
        //echo "ACTIVATED";
    } 
    add_action( 'activated_plugin', 'usocketnet_activate' );

    /**
     * Deactivation hook.
     */
    function usocketnet_deactivate() {
        //echo "DEACTIVATED";
    }
    register_deactivation_hook( __FILE__, 'usocketnet_deactivate' );

?>