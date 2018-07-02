class ResultEntryPreload extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            countDown: 3
        };

        this.preloadCountDown = this.preloadCountDown.bind(this);
    }

    preloadCountDown() {
        this.setState((prevState, props) => ({
            countDown: prevState.countDown - 1
        }));

        if (this.state.countDown == 0) {
            window.clearInterval(this.timer);

            $.ajax({
                url: "WaitForRewardType",
                method: "GET"
            }).success((data, textStatus, jqXHR) => {

                if (data.message == "ready") {
                    this.props.toggleState();
                }
                else if (data.message == "not-ready") {

                    this.setState({
                        countDown: 3
                    });
                    this.timer = window.setInterval(this.preloadCountDown, 1000);
                }

            });
        }
    }

    componentDidMount() {
        this.timer = window.setInterval(this.preloadCountDown, 1000);
    }

    render() {
        return (
            <div className="container">
                <div className="row">
                    <h4>กรุณารอผู้ตรวจสอบการบันทึกผลรางวัลเลือกประเภทรางวัล {this.state.countDown} วินาที</h4>
                </div>
            </div>
            );
    }
}