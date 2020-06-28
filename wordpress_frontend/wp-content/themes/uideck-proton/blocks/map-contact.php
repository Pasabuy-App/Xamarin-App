<?php
	/**
	 * UIDECK Proton blocks for maps and contacts.
	 *
	 * @package uideck-proton
	 * @since 0.1.0
	 */
?>

    <!-- Map Section Start -->
    <section id="map-area">
      <div class="container-fluid">
        <div class="row">
          <div class="col-12 padding-0">
            <object style="border:0; height: 450px; width: 100%;" data="<?php echo getThemeField( 'uid_googlemap', 'https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3865.69438588003!2d121.03918311631925!3d14.329176389975753!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3397d7accf8e5839%3A0xbfdc9ef48149ab86!2sBytes%20Crafter!5e0!3m2!1sen!2sph!4v1593358808233!5m2!1sen!2sph' ); ?>"></object>
          </div>
        </div>
      </div>
    </section>
    <!-- Map Section End -->

    <!-- Contact Section Start -->
    <section id="contact">      
      <div class="contact-form">
        <div class="container">
          <div class="row justify-content-center"> 
            <div class="offset-top">
              <div class="col-lg-12 col-md-12 col-xs-12">
                <div class="contact-block wow fadeInUp" data-wow-delay="0.2s">
                  <div class="section-header">   
                    <p class="btn btn-subtitle wow fadeInDown" data-wow-delay="0.2s">Contact</p>       
                    <h2 class="section-title">Your Thoughts is Important to Us!</h2>
                  </div>
                  <form id="contactForm">
                    <div class="row">
                      <div class="col-md-6">
                        <div class="form-group">
                          <input type="text" class="form-control" id="name" name="name" placeholder="Name" required data-error="Please enter your name">
                          <div class="help-block with-errors"></div>
                        </div>                                 
                      </div>
                      <div class="col-md-6">
                        <div class="form-group">
                          <input type="text" placeholder="Email" id="email" class="form-control" name="name" required data-error="Please enter your email">
                          <div class="help-block with-errors"></div>
                        </div> 
                      </div>
                      <div class="col-md-12">
                        <div class="form-group">
                          <input type="text" placeholder="Subject" id="msg_subject" class="form-control" required="" data-error="Please enter your subject">
                          <div class="help-block with-errors"></div>
                        </div>
                      </div>
                      <div class="col-md-12">
                        <div class="form-group"> 
                          <textarea class="form-control" id="message" placeholder="Message" rows="7" data-error="Write your message" required></textarea>
                          <div class="help-block with-errors"></div>
                        </div>
                        <div class="submit-button">
                          <button class="btn btn-common btn-effect" id="submit" type="submit">Submit</button>
                          <div id="msgSubmit" class="h3 hidden"></div> 
                          <div class="clearfix"></div> 
                        </div>
                      </div>
                    </div>            
                  </form>
                </div>
              </div>
            </div> 
          </div>
        </div>
      </div>        
    </section>
    <!-- Contact Section End -->