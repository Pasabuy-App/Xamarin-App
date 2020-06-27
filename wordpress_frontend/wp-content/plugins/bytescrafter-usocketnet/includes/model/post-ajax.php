
<?php
	// Exit if accessed directly
	if ( ! defined( 'ABSPATH' ) ) 
	{
		exit;
	}

	/** 
		* @package bytescrafter-usocketnet-restapi
		* Name: USocketNet RestAPI
		* Description: Self-Host Realtime Multiplayer Server 
		*       for your Game or Chat Application.
		* Package-Website: https://usocketnet.bytescrafter.net
		* 
		* Author: Bytes Crafter
		* Author-Website:: https://www.bytescrafter.net/about-us
		* License: Copyright (C) Bytes Crafter - All rights Reserved. 
	*/
?>

<?php

    //AJAX LISTENER for Apps
	include_once ( plugin_dir_path( __FILE__ ) . '/project/reload.php' );
	include_once ( plugin_dir_path( __FILE__ ) . '/project/create.php' );
	include_once ( plugin_dir_path( __FILE__ ) . '/project/update.php' );
    include_once ( plugin_dir_path( __FILE__ ) . '/project/delete.php' );
    
    //AJAX LISTENER for Projects
    include_once ( plugin_dir_path( __FILE__ ) . '/cluster/reload.php' );
    include_once ( plugin_dir_path( __FILE__ ) . '/cluster/create.php' );
    include_once ( plugin_dir_path( __FILE__ ) . '/cluster/update.php' );
    include_once ( plugin_dir_path( __FILE__ ) . '/cluster/delete.php' );

?>