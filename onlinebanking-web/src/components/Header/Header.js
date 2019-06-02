import React from 'react';
import "./Header.css";
import logo from "../../logo.png";

export default () => (
    <div className="header-box">
        <div className="logo-box">
            <img src={logo}></img>
        </div>
        <div className="header-box-menu"></div>
    </div>
);