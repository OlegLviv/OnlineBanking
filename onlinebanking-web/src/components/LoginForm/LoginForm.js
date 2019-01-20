import React from 'react';
import {
    Form, Icon, Input, Button, Checkbox, Select
} from 'antd';
import './LoginForm.css';
import classnames from 'classnames';

const { Option } = Select;

class LoginForm extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            phoneNumber: '',
            password: ''
        }
    }

    onSubmit = e => {
        e.preventDefault();
        console.log(this.props);
        this.props.form.validateFields().then(e=>console.log(e))
    }

    render() {
        const { getFieldDecorator } = this.props.form;

        const prefixSelector = getFieldDecorator('prefix', {
            initialValue: '+380',
        })(
            <Select style={{ width: 75 }}>
                <Option value="+380">+380</Option>
            </Select>
        );

        return (
            <Form className={classnames('login-form', this.props.className)}
                onSubmit={this.onSubmit}>
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
                    {getFieldDecorator('remember', {
                        valuePropName: 'checked',
                        initialValue: true,
                    })(
                        <Checkbox>Remember me</Checkbox>
                    )}
                    <a className="login-form-forgot" href="">Forgot password</a>
                    <Button type="primary" htmlType="submit" className="login-form-button">
                        Log in
          </Button>
                    Or <a href="">register now!</a>
                </Form.Item>
            </Form>
        );
    }
}

export default Form.create({
    mapPropsToFields: (props) => console.log('pr', props),

})(LoginForm);