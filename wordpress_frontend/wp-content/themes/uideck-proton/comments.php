
<?php
	/**
    * The template file for displaying the comments and comment form for the
	*
	* @package uideck-proton
	* @since 0.1.0
	*/
?>

<?php 
if ( post_password_required() ) {
	return;
}

if ( $comments ) {
	?>

	<div class="comments" id="comments">

		<?php
			$comments_number = absint( get_comments_number() );
		?>

		<div class="comments-header section-inner small max-percentage">

			<h2 class="comment-reply-title">
			<?php
			if ( ! have_comments() ) {
				_e( 'Leave a comment', 'uideck-proton' );
			} elseif ( '1' === $comments_number ) {
				/* translators: %s: Post title. */
				printf( _x( 'One reply on &ldquo;%s&rdquo;', 'comments title', 'uideck-proton' ), get_the_title() );
			} else {
				printf(
					/* translators: 1: Number of comments, 2: Post title. */
					_nx(
						'%1$s reply on &ldquo;%2$s&rdquo;',
						'%1$s replies on &ldquo;%2$s&rdquo;',
						$comments_number,
						'comments title',
						'uideck-proton'
					),
					number_format_i18n( $comments_number ),
					get_the_title()
				);
			}

			?>
			</h2>
		</div>

		
		<div class="comments-inner section-inner thin max-percentage">
			<?php
				wp_list_comments(
					array(
						'walker'      => new UIDECK_Proton_WalkerComment(),
						'avatar_size' => 60,
						'style'       => 'div',
						'reply_text'        => 'COMMENT',				
					)
				);

				$comment_pagination =  paginate_comments_links( array( 'echo' => false, 'type' => 'array' ));
		  
				if( is_array( $comment_pagination ) ) {
				  $output = '';
				  foreach ($comment_pagination as $page) {
						$page = "\n<li>$page</li>\n";
						if (strpos($page, ' current') !== false) {
							$page = str_replace([' current', '<li>'], ['', '<li class="active">'], $page);
						}
						$output .= $page;
						//print_r($page);
				  }
				  ?>
				  <nav aria-label="Comment navigation">
					  <ul class="pagination">
						  <?php echo $output; ?>
					  </ul>
				  </nav>
<?php
				}

?>
		</div>
	</div>
				
<?php
}

	// Remove the logout link in comment form
	add_filter( 'comment_form_logged_in', '__return_empty_string' );

	//#region COMMENT FORM HERE.
		if ( comments_open() || pings_open() ) {

			if ( $comments ) {
				echo '<hr class="styled-separator is-style-wide" aria-hidden="true" />';
			}

			if ( is_user_logged_in() ) {
				comment_form(
					array(
						'title_reply'        => 'Join the Conversation',
						'title_reply_to'     => 'Leave a Reply to %s',
						'class_form'         => 'comment-form',
						'title_reply_before' => '<h5 id="reply-title" class="comment-reply-title">',
						'title_reply_after'  => '</h2>',
						'comment_field'		 => '<textarea maxlength="200" class="comment-textfield" placeholder="What do you want to say?" name="comment" id="comment" ></textarea>',
						'label_submit'		 => 'SUBMIT COMMENT',
						'submit_button'		 => '<input name="%1$s" type="submit" id="%2$s" class="comment-submit" value="%4$s">',
						'cancel_reply_link'	 => 'CANCEL'
					)
				);

			} else {

			?>
				<div class="comment-respond" id="respond" style="text-align: center; color: red;">
					<p class="comments-closed" style="font-weight: 400;">
						<?php _e( 'You need to login first, anonymous users are not allowed.', 'uideck-proton' ); ?>
						<a href="<?php echo wp_login_url(); ?>" style="font-weight: 500;"> SIGN NOW </a>
					</p>
				</div>

			<?php

			}

		} elseif ( is_single() ) {

			if ( $comments ) {
				echo '<hr class="styled-separator is-style-wide" aria-hidden="true" />';
			}

			?>

				<div class="comment-respond" id="respond" style="text-align: center; color: red;">
					<p class="comments-closed"><?php _e( 'Comments are closed.', 'uideck-proton' ); ?></p>
				</div>

			<?php
		}
	//#endregion
?>