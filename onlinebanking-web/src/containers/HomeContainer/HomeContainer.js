import React from 'react';
import BaseContainer from '../BaseContainer';
import LoginForm from '../../components/LoginForm/LoginForm';
import './HomeContainer.css';

import { connect } from 'react-redux';
import { sendUserTwoFa } from '../../actions/users';
import { fetchToken } from '../../actions/token';
import { logIn } from '../../api/api';

import { Row, Col } from 'antd';

class HomeContainer extends BaseContainer {
    constructor(props) {
        super(props);
    }

    onSubmitTwoFa = e => this.props.send2FaCode({
        phoneNumber: `${e.prefix}${e.phoneNumber}`,
        password: e.password
    })
        .catch(err => {
            if (err.response && err.response.status === 400) {
                this.setState({ errorMessage: err.response.data });
            }
        });

    onTokenSubmit = (userId, code) => {
        this.props
            .fetchToken(userId, code)
            .then(resp => {
                if (resp.token && resp.token.accessToken) {
                    logIn(resp.token.accessToken, '/user');
                }
            })
            .catch(err => {
                console.log(err);
                if (err.response && err.response.status === 400) {
                    this.setState({ errorMessage: err.response.data });
                }
            });
    }

    render() {
        const { userState, tokenState } = this.props;
        return (
            <Row type="flex" justify="space-between">
                <Col span={12}>
                </Col>
                <Col span={8}>
                    <LoginForm
                        loading={userState.loading || tokenState.loading}
                        onSubmitTwoFa={this.onSubmitTwoFa}
                        onTokenSubmit={this.onTokenSubmit} />
                </Col>
            </Row>
        );
    }
}

export default connect(
    state => ({
        userState: state.users,
        tokenState: state.token
    }),
    dispatch => ({
        send2FaCode: body => dispatch(sendUserTwoFa(body)),
        fetchToken: (userId, code) => dispatch(fetchToken(userId, code))
    })
)(HomeContainer);