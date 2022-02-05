# A Posture Recognizer Based on Kinect

This is a little project for a students' innovative entrepreneurial training program, 
completed during my sophomore year to junior year (2018-2019).

Note that we only uploaded the core src files here.

So the project **cannot be successfully installed** with these files.

# Introduction
[Kinect](https://en.wikipedia.org/wiki/Kinect) is a somatosensory device introduced by Microsoft that has created a stir in the field of computer vision with its much lower price and reliable performance than traditional devices. It is a composite device consisting of an infrared laser emitter, an infrared camera and an RGB camera.

This project makes use of the skeleton-tracking feature of Kinect to define and reuse human posture by analyzing the gained data.
Specifically, we structure the data of 25 joints captured by the device in frames, 
extract the data features by vector angle calculation, etc., 
and thus realize storage, presentation, and recognition of human posture in real-time.

It is worthy to mention that during the implementation, we have observed obvious jitter due to the instability of the equipment.
To alleviate this problem, we add a mean filter with an adjustable sliding window and a Kalman filter to the program.
As the result, the jitter is mitigated to a large degree.

## The overview of this system
![](https://gyazo.com/90d58493c50acfb61f9d086beb67d3fd.png)

# To use
1. Select a recorded posture file as a template.
2. Input a posture for matching with the selected template.
	-	You can both input it in real-time with the device, or instead, load a recorded posture file.
	-	You can select a body part (e.g., the left arm) to specifically focus on if necessary.
3. Check the matching score, which shows the similarity between the template and the input.
4. You could record both templates and inputs so as to be loaded next time.
