
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

	<?php /* Header Section */ ?>
		<div class="usn-welcome-header">
			<h1>USocket Net</h1>
			<p>Realtime WebSocket Multiplayer Server for Indie Game Developers.
			<br> It is a multi-platform that can be used through mobile or standalone computers.
			<br> Optional! Host your own server, contact us now.</p>
		</div>
	<?php /* Header Section */ ?>

	<div class="jumbotron usn-content" style="text-align: center;">
		<p class="lead">
			Microservice architecture, Load Balance, Realtime server and connection stats, and many more.
		</p>
		<hr>
		<h5>
			Short Description: Self-Host Realtime Bidirectional Event-Based Communication for your Game or Chat Application.
		</h5>
		<p style="font-size: 17px;">
			The USocketNet is currently designed and developed for Unity Engine. It is a multi-platform by design 
			that can be used through mobile, computers, or even web. We advised the developers to report any issues 
			or bugs immediately to further fix and improve our code. We are driven to add new features that will 
			make this project awesome for everyone.
		</p>
		<a class="btn btn-dark btn-lg" href="https://usocketnet.bytescrafter.net" target="_blank" role="button">LEARN MORE</a>
	</div>

	<div class="usn-content">

		<h4 style="text-align: center;">Made Possible by this Technologies</h4>
		<p class="usn-align-center">
			The USocketNet can be deploy on Google Cloud, Amazon AWS, etc. The operating system is preferably in a Linux environment, either Debian or centos as a production server. We will provide thorough documentation about the deployment of USocketNet in any possible way.
		</p>
		<div class="container">
			<div class="row">
				<div class="col-md-3 card">
					<img class="card-img-top" src="<?php echo USN_PLUGIN_URL; ?>assets/images/USN-Nginx.jpg" alt="USocketNet">
					<div class="card-body">
						<p class="card-text">
							Nginx is a web server which can also be used as a reverse proxy, load balancer, mail proxy and HTTP cache. 
						</p>
					</div>
				</div>
				<div class="col-md-3 card">
					<img class="card-img-top" src="<?php echo USN_PLUGIN_URL; ?>assets/images/USN-NodeJS.jpg" alt="USocketNet">
					<div class="card-body">
						<p class="card-text">
							Node.js is an open-source, cross-platform JavaScript run-time environment that executes JavaScript code outside of a browser.
						</p>
					</div>
				</div>
				<div class="col-md-3 card">
					<img class="card-img-top" src="<?php echo USN_PLUGIN_URL; ?>assets/images/USN-WordPress.jpg" alt="USocketNet">
					<div class="card-body">
						<p class="card-text">
							WordPress is a free and open-source content management system based on PHP and MySQL. Features include a plugin architecture and a template system.
						</p>
					</div>
				</div>
				<div class="col-md-3 card">
					<img class="card-img-top" src="<?php echo USN_PLUGIN_URL; ?>assets/images/USN-Unity.jpg" alt="USocketNet">
					<div class="card-body">
						<p class="card-text">
							Unity, the world's leading real-time engine, is used to create half of the world's games. Our flexible real-time tools offer incredible possibilities.
						</p>
					</div>
				</div>
			</div>
		</div>
	</div>