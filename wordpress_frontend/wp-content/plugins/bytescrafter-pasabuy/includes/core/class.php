
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
    
    function ps_get_avatar_url($user_id)
    {
        $avatar_image_url = get_avatar_url($user_id, array('size' => 150));

        if ( strpos($avatar_image_url, 'www.gravatar.com/avatar/') !== false ) {
            $avatar_image_url = PS_PLUGIN_URL."assets/images/default-avatar.png";
        }

        $avatar_image_url = str_replace(home_url(), '', $avatar_image_url);

        return $avatar_image_url;
    }

    //Get the cover url.
    function ps_get_banner_url($user_id)
    {
        $banner_image_url = bp_attachments_get_attachment( 'url', array( 'item_id' => $user_id ) );

        if ( !$banner_image_url ) {
            $banner_image_url = PS_PLUGIN_URL."assets/images/default-banner.png";
        }

        $banner_image_url = str_replace(home_url(), '', $banner_image_url);

        return $banner_image_url;
    }

?>