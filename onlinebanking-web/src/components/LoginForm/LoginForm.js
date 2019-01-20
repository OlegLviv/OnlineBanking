import React from 'react';
import classnames from 'classnames';
import './LoginForm.css';

import {
    Form,
    Icon,
    Input,
    Button,
    Alert,
    Select,
    Spin
} from 'antd';
import { Link } from 'react-router-dom';

const { Option } = Select;

class LoginForm extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            main: true
        }
    }

    onSubmitTwoFa = e => {
        e.preventDefault();

        this.props
            .form
            .validateFields()
            .then(e => {
                this.props
                    .onSubmitTwoFa(e)
                    .then(resp => {
                        if (resp && resp.twoFactorInfo && resp.twoFactorInfo.userId) {
                            console.log('r', resp);
                            this.setState({ main: false })
                            this.userId = resp.twoFactorInfo.userId;
                            this.userRoles = resp.twoFactorInfo.roles;
                        }
                    });
            });
    }

    onTokenSubmit = e => {
        e.preventDefault();

        this.props
            .form
            .validateFields()
            .then(e => {
                this.props
                    .onTokenSubmit(this.userId, e.code, this.userRoles);
            });
    }

    renderMainForm = (getFieldDecorator, prefixSelector, loading) => (
        <Spin spinning={loading}>
            <Form className={classnames('login-form', this.props.className)}
                onSubmit={this.onSubmitTwoFa}>
                <Form.Item>
                    {getFieldDecorator('phoneNumber', {
                        rules: [{ required: true, message: 'Please input your phone' }],
                    })(
                        <Input addonBefore={prefixSelector} placeholder="Phone" />
                    )}
                </Form.Item>
                <Form.Item>
                    {getFieldDecorator('password', {
                        rules: [{ required: true, message: 'Please input your Password!' }],
                    })(
                        <Input prefix={<Icon type="lock" style={{ color: 'rgba(0,0,0,.25)' }} />} type="password" placeholder="Password" />
                    )}
                </Form.Item>
                <Form.Item>
                    <Link className="login-form-forgot" to="">Forgot password</Link>
                    <Button type="primary"
                        htmlType="submit"
                        className="login-form-button"
                        disabled={loading}>
                        Log in
                    </Button>
                </Form.Item>
            </Form>
        </Spin>
    );

    render2FaCodeForm = (getFieldDecorator, loading) => (
        <Spin spinning={loading}>
            <Form onSubmit={this.onTokenSubmit}>
                <Form.Item>
                    <Alert message="Information!"
                        description="We have sent confirmation code to your email. Please check your email box and input code below"
                        type="info" />
                </Form.Item>
                <Form.Item>
                    {getFieldDecorator('code', {
                        rules: [{
                            required: true,
                            message: 'Please input your Code!',
                            pattern: /^[0-9]{6}$/
                        }],
                    })(
                        <Input prefix={<Icon type="lock"
                            style={{ color: 'rgba(0,0,0,.25)' }} />}
                            type="number"
                            placeholder="Code" />
                    )}
                </Form.Item>
                <Form.Item>
                    <Button type="primary"
                        htmlType="submit"
                        className="login-form-button"
                        disabled={loading}>
                        Send
                    </Button>
                </Form.Item>
            </Form>
        </Spin>
    );

    render() {
        const { getFieldDecorator } = this.props.form;
        const { loading } = this.props;

        const prefixSelector = getFieldDecorator('prefix', {
            initialValue: '+380',
        })(
            <Select style={{ width: 75 }}>
                <Option value="+380">+380</Option>
            </Select>
        );

        if (this.state.main)
            return this.renderMainForm(getFieldDecorator, prefixSelector, loading);

        return this.render2FaCodeForm(getFieldDecorator, loading);
    }
}

export default Form.create()(LoginForm);