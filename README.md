## Table of Contents

* [General Information](#general-information)
* [Technologies Used](#technologies-used)
* [Project Status](#project-status)
* [Feature List](#feature-list)
* [Changelog](#changelog)

# General Information

A PONG clone intented for practicing Unity development and incremental releases. Also being used as a more refined version of my high school senior year project, Space PONG.

## Technologies Used

* Unity
* C#
* Visual Studio

## Project Status

This project was originally created as a simple exercise in 2018 and is currently under development as part of my self-imposed one game a month (1GAM) challenge for 2021.

## Feature List

* Basic PONG Gameplay: Two players compete to score goals by deflecting the ball with their paddles.
* Control Ball Motion: Hitting the ball with the center of the paddle will send it directly toward the opponent, while hitting closer to the edge of the paddle will send it back at an angle.

## Changelog

### v0.2

* Fixed ball launch code to avoid vertical launches and aim more directly at one of the players
* Added ability to influence ball movement by hitting it with different parts of the paddle
* Updated the paddle motion to remove acceleration and deceleration when receiving movement input
* Improved README

### v0.1

* Simple implementation with two player-controlled paddles and a single-score win condition
